using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;
using LASI.Core.Configuration;
using LASI.Core.Analysis.Heuristics.Support;

namespace LASI.Core.Analysis.Heuristics
{
    /// <summary>
    /// Contains methods which compute and yield the top results from a document based on simple, naive heuristics.
    /// </summary>
    public static class NaiveTopResultSelector
    {
        private static IEnumerable<Pair<string, float>> GetTopResultsByVerbal(IReifiedTextual source) =>
            from svs in GetVerbWiseRelationships(source)
            let dataPoint = new
            {
                Key = $@"{svs.Subject.Text} -> {svs.Verbal.Text}
                         {(svs.Direct != null ? " -> " + svs.Direct.Text : string.Empty)}
                         {(svs.Indirect != null ? " -> " + svs.Indirect.Text : string.Empty)}",
                Value = (float)Math.Round(svs.Weight, 2)
            }
            group dataPoint by dataPoint into pointGroup
            select Pair.Create(pointGroup.Key.Key, pointGroup.Key.Value);

        private static IEnumerable<SvoRelationship> GetVerbWiseRelationships(IReifiedTextual source)
        {
            var relationships =
                from verbal in source.Phrases
                    .OfVerbPhrase()
                    .AsParallel()
                    .WithDegreeOfParallelism(Concurrency.Max)
                    .WithSubject(s => !(s is IReferencer) || (s as IReferencer).RefersTo != null)
                    .Distinct((x, y) => x.IsSimilarTo(y))
                from entity in verbal.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                let subject = entity.Match()
                     .When((IReferencer r) => r.RefersTo?.Any() ?? false)
                     .Then((IReferencer r) => r.RefersTo)
                     .Result(entity)
                from direct in verbal.DirectObjects.DefaultIfEmpty()
                from indirect in verbal.IndirectObjects.DefaultIfEmpty()
                where subject != null && (direct ?? indirect) != null /*&& (subject.Text != (direct ?? indirect).Text)*/
                let relationship = new SvoRelationship(subject, verbal, direct, indirect)
                orderby relationship.Weight descending
                select relationship;
            return relationships.Distinct();
        }
        /// <summary>
        /// Returns the top results for the given document using a heuristic which emphasizes the occurrence of Entities above
        /// other metrics.
        /// </summary>
        /// <param name="source">The Document from which to retrieve results.</param>
        /// <returns>The top results for the given document using a heuristic which emphasizes the occurrence of Entities above
        /// other metrics.</returns>
        public static IEnumerable<Pair<string, float>> GetTopResultsByEntity(IReifiedTextual source)
        {
            var results = from entity in source.Phrases.OfEntity()
                         .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                          let e = entity.Match()
                            .When((IReferencer r) => r.RefersTo?.Any() ?? false)
                            .Then((IReferencer r) => r.RefersTo)
                            .Result(entity)
                          where e != null
                          group new { Name = e.Text, Value = (float)Math.Round(e.Weight, 2) } by e.Text into g
                          where g.Any()
                          let topOfGroup = g.MaxBy(x => x.Value)
                          orderby topOfGroup.Value descending
                          select Pair.Create(topOfGroup.Name, topOfGroup.Value);
            return results;
        }
    }
}
