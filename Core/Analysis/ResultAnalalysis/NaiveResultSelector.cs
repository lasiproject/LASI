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

        private static IEnumerable<KeyValuePair<string, float>> GetTopResultsByVerbal(Document doc) {
            var data = GetVerbWiseRelationships(doc);
            return from svs in data
                   let SV = new KeyValuePair<string, float>(
                       string.Format("{0} -> {1}\n", svs.Subject.Text, svs.Verbal.Text) +
                       (svs.Direct != null ? " -> " + svs.Direct.Text : "") +
                       (svs.Indirect != null ? " -> " + svs.Indirect.Text : ""),
                       (float)Math.Round(svs.CombinedWeight, 2))
                   group SV by SV into svg
                   select svg.Key;

        }
        private static IEnumerable<SvoRelationship> GetVerbWiseRelationships(Document doc) {
            var data =
                 from svPair in
                     (from vp in doc.Phrases.OfVerbPhrase()
                          .WithSubject(s => (s as IReferencer) == null || (s as IReferencer).ReferesTo != null).Distinct((x, y) => x.IsSimilarTo(y))
                          .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      from s in vp.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      let sub = s as IReferencer == null ? s : (s as IReferencer).ReferesTo
                      where sub != null
                      from dobj in vp.DirectObjects.DefaultIfEmpty()
                      from iobj in vp.IndirectObjects.DefaultIfEmpty()

                      select new SvoRelationship {
                          Subject = vp.AggregateSubject,
                          Verbal = vp,
                          Direct = vp.AggregateDirectObject,
                          Indirect = vp.AggregateIndirectObject,
                          Prepositional = vp.ObjectOfThePreoposition,
                          CombinedWeight = sub.Weight + vp.Weight + (dobj != null ? dobj.Weight : 0) + (iobj != null ? iobj.Weight : 0)
                      } into tupple
                      where
                      tupple.Subject != null &&
                        tupple.Direct != null ||
                        tupple.Indirect != null &&
                        tupple.Subject.Text != (tupple.Direct ?? tupple.Indirect).Text
                      select tupple).Distinct()
                 select svPair into svps
                 orderby svps.CombinedWeight descending
                 select svps;
            return data;
        }
        /// <summary>
        /// Returns top results for the given document using a heuristic which emphasises the occurence of Entities above other metrics.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, float>> GetTopResultsByEntity(Document document) {
            return from entity in document.Phrases.OfEntity()
                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                   orderby entity.Weight descending
                   let e = entity.Match().Yield<IEntity>()
                       .With<IReferencer>(r => r.ReferesTo != null && r.ReferesTo.Any() ? r.ReferesTo : null)
                       .With<IEntity>(entity)
                   .Result()
                   where e != null
                   group e by new {
                e.Text,
                e.Weight
                   } into entity
                   select entity.Key into master
                   select new KeyValuePair<string, float>(master.Text, (float)Math.Round(master.Weight, 2)) into item
                   group item by item.Key into g
                   select g.MaxBy(x => x.Value);
        }
    }
}
