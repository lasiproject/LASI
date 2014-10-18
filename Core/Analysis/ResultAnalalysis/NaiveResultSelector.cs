using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;
using LASI.Core.Interop;

namespace LASI.Core
{
    /// <summary>
    /// Contains methods which compute and yield the top results from a document based on simple, naive heuristics.
    /// </summary>
    public static class NaiveResultSelector
    {
        private static IEnumerable<dynamic> GetTopResultsByVerbal(Document doc) {
            var data = GetVerbWiseRelationships(doc);
            return from svs in data
                   let dataPoint = new
                   {
                       Key =
                       string.Format("{0} -> {1}\n", svs.Subject.Text, svs.Verbal.Text) +
                       (svs.Direct != null ? " -> " + svs.Direct.Text : string.Empty) +
                       (svs.Indirect != null ? " -> " + svs.Indirect.Text : string.Empty),
                       Value = (float)Math.Round(svs.CombinedWeight, 2)
                   }
                   group dataPoint by dataPoint into pointGroup
                   select pointGroup.Key;

        }
        private static IEnumerable<SvoRelationship> GetVerbWiseRelationships(Document doc) {
            var data = from verbal in doc.Phrases.OfVerbPhrase().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                         .WithSubject(s => (s as IReferencer) == null || (s as IReferencer).RefersTo != null).Distinct((x, y) => x.IsSimilarTo(y))
                       from entity in verbal.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                       let subject = entity.Match().Yield<IEntity>()
                            .When((IReferencer r) => r.RefersTo != null && r.RefersTo.Any())
                            .Then((IReferencer r) => r.RefersTo)
                            .Result(entity)
                       from direct in verbal.DirectObjects.DefaultIfEmpty()
                       from indirect in verbal.IndirectObjects.DefaultIfEmpty()
                       let relationship = new SvoRelationship(verbal.AggregateSubject, verbal, verbal.AggregateDirectObject, verbal.AggregateIndirectObject)
                       where relationship.Subject != null &&
                         relationship.Direct != null ||
                         relationship.Indirect != null &&
                         relationship.Subject.Text != (relationship.Direct ?? relationship.Indirect).Text
                       orderby relationship.CombinedWeight descending
                       select relationship;
            return data.Distinct();
        }
        /// <summary>
        /// Returns the top results for the given document using a heuristic which emphasises the occurence of Entities above
        /// other metrics.
        /// </summary>
        /// <param name="document">The Document from which to retrieve results.</param>
        /// <returns>The top results for the given document using a heuristic which emphasises the occurence of Entities above
        /// other metrics.</returns>
        public static IEnumerable<dynamic> GetTopResultsByEntity(Document document) {
            return from entity in document.Phrases.OfEntity()
                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                   orderby entity.Weight descending
                   let e = entity.Match().Yield<IEntity>()
                       .With((IReferencer r) => r.RefersTo != null && r.RefersTo.Any() ? r.RefersTo : entity)
                   .Result()
                   group new { Key = e.Text, Value = (float)Math.Round(e.Weight, 2) } by e.Text into g
                   select g.DefaultIfEmpty().MaxBy(x => x.Value);
        }
    }
}
