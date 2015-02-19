using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;
using LASI.Core.Reporting;

namespace LASI.Core
{
    /// <summary>
    /// Contains methods which compute and yield the top results from a document based on simple, naive heuristics.
    /// </summary>
    public static class NaiveResultSelector
    {
        private static IEnumerable<Pair<string, float>> GetTopResultsByVerbal(IReifiedTextual source)
        {
            var data = GetVerbWiseRelationships(source);
            return from svs in data
                   let dataPoint = new
                   {
                       Key = $"{svs.Subject.Text} -> {svs.Verbal.Text}\n" + 
                                ((svs.Direct != null ? " -> " + svs.Direct.Text : string.Empty) +
                                (svs.Indirect != null ? " -> " + svs.Indirect.Text : string.Empty)),
                       Value = (float)Math.Round(svs.Weight, 2)
                   }
                   group dataPoint by dataPoint into pointGroup
                   let key = pointGroup.Key
                   select Pair.Create(key.Key, key.Value);

        }
        private static IEnumerable<SvoRelationship> GetVerbWiseRelationships(IReifiedTextual source)
        {
            var data = from verbal in source.Phrases.OfVerbPhrase().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                         .WithSubject(s => (s as IReferencer) == null || (s as IReferencer).RefersTo != null).Distinct((x, y) => x.IsSimilarTo(y))
                       from entity in verbal.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                       let subject = entity.Match()/*.Yield<IEntity>()*/
                            .When((IReferencer r) => r.RefersTo != null && r.RefersTo.Any())
                            .Then((IReferencer r) => r.RefersTo)
                            .Result() ?? entity
                       from direct in verbal.DirectObjects.DefaultIfEmpty()
                       from indirect in verbal.IndirectObjects.DefaultIfEmpty()
                       let relationship = new SvoRelationship(verbal.AggregateSubject, verbal, verbal.AggregateDirectObject, verbal.AggregateIndirectObject)
                       where relationship.Subject != null &&
                         relationship.Direct != null ||
                         relationship.Indirect != null &&
                         relationship.Subject.Text != (relationship.Direct ?? relationship.Indirect).Text
                       orderby relationship.Weight descending
                       select relationship;
            return data.Distinct();
        }
        /// <summary>
        /// Returns the top results for the given document using a heuristic which emphasises the occurence of Entities above
        /// other metrics.
        /// </summary>
        /// <param name="source">The Document from which to retrieve results.</param>
        /// <returns>The top results for the given document using a heuristic which emphasises the occurence of Entities above
        /// other metrics.</returns>
        public static IEnumerable<Pair<string, float>> GetTopResultsByEntity(IReifiedTextual source)
        {
            var results = from entity in source.Phrases.OfEntity()
                         .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                          orderby entity.Weight descending
                          let e = entity.Match()/*.Yield<IEntity>()*/
                              .Case((IReferencer r) => r.RefersTo != null && r.RefersTo.Any() ? r.RefersTo : entity)
                          .Result(entity)
                          where e != null
                          group new { Name = e.Text, Value = (float)Math.Round(e.Weight, 2) } by e.Text into g
                          where g.Any()
                          select g.MaxBy(x => x.Value) into result
                          select Pair.Create(result.Name, result.Value);
            return results;
        }
    }
}
