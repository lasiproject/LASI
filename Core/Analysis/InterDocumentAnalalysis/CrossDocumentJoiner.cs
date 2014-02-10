using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.Core.Patternization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Facilitates the joining of multiple documents into a single result set based on overlap and intersection tequniques.
    /// </summary>
    public class CrossDocumentJoiner
    {
        /// <summary>
        /// Asynchrnously builds and computes the intersection of the given Documents contained and returns the results as a sequence of Relationship instances.
        /// </summary>
        /// <param name="documents">The Documents to Join.</param>
        /// <returns>A Task&lt;IEnumerable&lt;RelationshipTuple&gt;&gt; corresponding to the intersection of the Documents to be joined .</returns>
        public async Task<IEnumerable<SVORelationship>> GetCommonResultsAsnyc(IEnumerable<Document> documents) {
            return await await Task.Factory.ContinueWhenAll(
                new[] {  
                    Task.Run(()=> GetCommonalitiesByVerbals(documents)),
                    Task.Run(()=> GetCommonalitiesByEntities(documents))
                },
                async tasks => {
                    var results = new List<SVORelationship>();
                    foreach (var t in tasks) {
                        results.AddRange(await t);
                    }
                    return results.Distinct();
                }
            );
        }
        /// <summary>
        /// Builds and computes the intersection of the given Documents contained and returns the results as a sequence of Relationship instances.
        /// </summary>
        /// <param name="documents">The Documents to Join.</param>
        /// <returns>A Task&lt;IEnumerable&lt;RelationshipTuple&gt;&gt; corresponding to the intersection of the Documents to be joined .</returns>
        public IEnumerable<SVORelationship> GetCommonResults(IEnumerable<Document> documents) {
            return GetCommonResultsAsnyc(documents).Result;
        }
        private async Task<IEnumerable<SVORelationship>> GetCommonalitiesByEntities(IEnumerable<Document> documents) {
            await Task.Yield();
            var topNPsByDoc = from doc in documents
                               .AsParallel()
                               .WithDegreeOfParallelism(Concurrency.Max)
                              select new { TopNounPhrases = GetTopNounPhrases(doc), Document = doc };

            await Task.Yield();
            var crossReferenced = from o in topNPsByDoc
                                  from i in topNPsByDoc
                                  where i.Document != o.Document
                                  from np in o.TopNounPhrases
                                  where i.TopNounPhrases.Contains(np, CompareNps)
                                  select np;
            await Task.Yield();
            return from n in crossReferenced.Distinct(CompareNps)
                   orderby n.SubjectOf.Text
                   select new SVORelationship {
                       Verbal = n.SubjectOf,
                       Subject = new AggregateEntity(new[] { n }),
                       Direct = new AggregateEntity(n.SubjectOf.DirectObjects),
                       Indirect = new AggregateEntity(n.SubjectOf.IndirectObjects),
                       Prepositional = n.SubjectOf.ObjectOfThePreoposition
                   };
        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(Document document) {
            return document.Phrases
                       .AsParallel()
                       .WithDegreeOfParallelism(Concurrency.Max)
                       .OfNounPhrase()
                       .InSubjectRole()
                       .InObjectRole()
                       .Distinct(CompareNps)
                       .OrderBy(np => np.Weight);
        }
        private async Task<IEnumerable<SVORelationship>> GetCommonalitiesByVerbals(IEnumerable<Document> documents) {
            var topVerbalsByDoc = await Task.WhenAll(from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                                     select GetTopVerbPhrasesAsync(doc));
            var verbalCominalities = from verbals in topVerbalsByDoc
                                     from v in verbals
                                     where (
                                        from verbs in topVerbalsByDoc
                                        select verbs.Contains(v, (x, y) => x.Text == y.Text || y.IsSimilarTo(y)))
                                     .Aggregate(true, (product, result) => product && result)
                                     select v;
            return from verbal in verbalCominalities
                   let testPronouns = new Func<IEnumerable<IEntity>, AggregateEntity>(
                   entities => new AggregateEntity(from s in entities let asPro = s as IReferencer select asPro != null ? asPro.ReferredTo.Any() ? asPro.ReferredTo : s : s))
                   select new SVORelationship {
                       Verbal = verbal,
                       Subject = testPronouns(verbal.Subjects),
                       Direct = testPronouns(verbal.DirectObjects),
                       Indirect = testPronouns(verbal.IndirectObjects),
                       Prepositional = verbal.ObjectOfThePreoposition
                   } into result
                   where result.Subject.Any()
                   orderby result.Verbal.Weight
                   group result by result.Verbal.Text into g
                   select g.First();
        }
        private async Task<ParallelQuery<VerbPhrase>> GetTopVerbPhrasesAsync(Document document) {
            return await Task.Run(() =>
                from vp in document.Phrases
                    .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                    .OfVerbPhrase()
                    .WithSubject().WithObject().Distinct((x, y) => x.IsSimilarTo(y))
                orderby vp.Weight + vp.Subjects.Sum(e => e.Weight) + vp.DirectObjects.Sum(e => e.Weight) + vp.IndirectObjects.Sum(e => e.Weight)
                select vp);
        }

        private static bool CompareNps(NounPhrase x, NounPhrase y) {
            return
                ReferencerTestCompare(x, y) ||
                ReferencerTestCompare(y, x) ||
                x.Text == y.Text ||
                x.IsAliasFor(y) || x.IsSimilarTo(y);
        }
        private class NPComparer : IEqualityComparer<NounPhrase>
        {
            private NPComparer() { }
            private static readonly NPComparer comparer = new NPComparer();
            public static NPComparer Instance { get { return comparer; } }
            public bool Equals(NounPhrase x, NounPhrase y) {
                return CompareNps(x, y);
            }
            public int GetHashCode(NounPhrase obj) {
                return obj.Text.GetHashCode();
            }
        }
        private static bool ReferencerTestCompare(NounPhrase x, NounPhrase y) {
            return x.Match().Yield<bool>().With<IReferencer>(r => r.ReferredTo.Any(e =>
                                e.Text == y.Text ||
                                e.IsAliasFor(y) ||
                                e.IsSimilarTo(y))).Result();
        }
        private static bool CompareNounPhrasesOld(NounPhrase x, NounPhrase y) {
            var leftAsPro = x as IReferencer;
            var rightAsPro = y as IReferencer;
            var result = rightAsPro != null && x.Referees.Contains(rightAsPro) || leftAsPro != null && y.Referees.Contains(leftAsPro);

            if (!result) {
                result = x.Text == y.Text || x.IsAliasFor(y) || x.IsSimilarTo(y);
            }
            return result;
        }
    }

}
