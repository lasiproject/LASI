using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;

namespace LASI.Core
{
    public static class NaiveResultSelector
    {
        #region General Chart Building Methods


        #endregion
        public static IEnumerable<KeyValuePair<string, float>> GetTopResultsByVerbal(Document doc) {
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
        private static IEnumerable<SVORelationship> GetVerbWiseRelationships(Document doc) {
            var data =
                 from svPair in
                     (from vp in doc.Phrases.OfVerbPhrase()
                          .WithSubject(s => (s as IReferencer) == null || (s as IReferencer).ReferredTo != null).Distinct((x, y) => x.IsSimilarTo(y))
                          .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      from s in vp.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      let sub = s as IReferencer == null ? s : (s as IReferencer).ReferredTo
                      where sub != null
                      from dobj in vp.DirectObjects.DefaultIfEmpty()
                      from iobj in vp.IndirectObjects.DefaultIfEmpty()

                      select new SVORelationship {
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

        public static IEnumerable<KeyValuePair<string, float>> GetTopResultsByEntity(Document doc) {
            return from entity in doc.Phrases.OfEntity()
                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                   orderby entity.Weight descending
                   let e = entity.Match().Yield<IEntity>()
                       .With<IReferencer>(r => r.ReferredTo != null && r.ReferredTo.Any() ? r.ReferredTo : null)
                       .With<IEntity>(entity)
                   .Result()
                   where e != null
                   group e by new
                   {
                       e.Text,
                       e.Weight
                   } into entity
                   select entity.Key into master
                   select new KeyValuePair<string, float>(master.Text, (float)Math.Round(master.Weight, 2)) into item
                   group item by item.Key into g
                   select g.MaxBy(x => x.Value);
        }






        /// <summary>
        /// Creates and returns a sequence of textual Display elements from the given sequence of RelationshipTuple elements.
        /// The resulting sequence is suitable for direct insertion into a DataGrid.
        /// </summary>
        /// <param name="elementsToConvert">The sequence of Relationship Tuple to tranform into textual Display elements.</param>
        /// <returns>A sequence of textual Display elements from the given sequence of RelationshipTuple elements.</returns>
        internal static IEnumerable<object> TransformToGrid(IEnumerable<SVORelationship> elementsToConvert) {
            return from e in elementsToConvert.Distinct()
                   orderby e.CombinedWeight
                   select new
                   {
                       Subject = e.Subject != null ? e.Subject.Text : string.Empty,
                       Verbal = e.Verbal != null ?
                                (e.Verbal.PrepositionOnLeft != null ? e.Verbal.PrepositionOnLeft.Text : string.Empty)
                                + (e.Verbal.Modality != null ? e.Verbal.Modality.Text : string.Empty)
                                + e.Verbal.Text + (e.Verbal.Modifiers.Any() ? " (adv)> "
                                + string.Join(" ", e.Verbal.Modifiers.Select(m => m.Text)) : string.Empty) :
                                string.Empty,
                       Direct = e.Direct != null ?
                                (e.Direct.PrepositionOnLeft != null ? e.Direct.PrepositionOnLeft.Text
                                : string.Empty + e.Direct.Text) :
                                string.Empty,
                       Indirect = e.Indirect != null ?
                                (e.Indirect.PrepositionOnLeft != null ? e.Indirect.PrepositionOnLeft.Text : string.Empty)
                                + e.Indirect.Text :
                                string.Empty

                   };
        }
    }
}
