using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.Core.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using LASI.Core.Interop;

namespace LASI.Core
{
    /// <summary>
    /// Facilitates the joining of multiple documents into a single result set based on overlap and intersection techniques.
    /// </summary>
    public class CrossDocumentJoiner
    {
        /// <summary>
        /// Asynchronously builds and computes the intersection of the given Documents contained and returns the results as a sequence of Relationship instances.
        /// </summary>
        /// <param name="documents">The Documents to Join.</param>
        /// <returns>A Task&lt;IEnumerable&lt;RelationshipTuple&gt;&gt; corresponding to the intersection of the Documents to be joined .</returns>
        public async Task<IEnumerable<SvoRelationship>> GetCommonResultsAsnyc(IEnumerable<Document> documents) {
            return await await Task.Factory.ContinueWhenAll(
                new[] {
                    Task.Run(()=> GetCommonalitiesByVerbals(documents)),
                    Task.Run(()=> GetCommonalitiesByEntities(documents))
                },
                async tasks => {
                    var results = new List<SvoRelationship>();
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
        public IEnumerable<SvoRelationship> GetCommonResults(IEnumerable<Document> documents) {
            return GetCommonResultsAsnyc(documents).Result;
        }
        private async Task<IEnumerable<SvoRelationship>> GetCommonalitiesByEntities(IEnumerable<Document> documents) {
            await Task.Yield();
            var topNPsByDoc = from doc in documents
                               .AsParallel()
                               .WithDegreeOfParallelism(Concurrency.Max)
                              select new { TopNounPhrases = GetTopNounPhrases(doc), Document = doc };

            await Task.Yield();
            var crossReferenced = from o in topNPsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                  from i in topNPsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                  where i.Document != o.Document
                                  from np in o.TopNounPhrases
                                  where i.TopNounPhrases.Contains(np, CompareNps)
                                  select np;
            await Task.Yield();
            return from n in crossReferenced.Distinct(CompareNps)
                   orderby n.SubjectOf.Text
                   select new SvoRelationship {
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
        private async Task<IEnumerable<SvoRelationship>> GetCommonalitiesByVerbals(IEnumerable<Document> documents) {
            var topVerbalsByDoc = await Task.WhenAll(from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                                     select GetTopVerbPhrasesAsync(doc));
            var verbalCominalities = from verbals in topVerbalsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                     from verbal in verbals
                                     where (from verbs in topVerbalsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                            select verbs.Contains(verbal, (x, y) => x.Text == y.Text || y.IsSimilarTo(y)))
                                            .All(x => x)
                                     select verbal;
            var relationships = from verbal in verbalCominalities
                                let testPronouns = new Func<IEnumerable<IEntity>, AggregateEntity>(
                                entities => new AggregateEntity(from s in entities let asPro = s as IReferencer select asPro != null ? asPro.ReferesTo.Any() ? asPro.ReferesTo : s : s))
                                let relationship = new SvoRelationship {
                                    Verbal = verbal,
                                    Subject = testPronouns(verbal.Subjects),
                                    Direct = testPronouns(verbal.DirectObjects),
                                    Indirect = testPronouns(verbal.IndirectObjects),
                                    Prepositional = verbal.ObjectOfThePreoposition
                                }
                                where relationship.Subject != null
                                orderby relationship.Verbal.Weight
                                select relationship;
            return relationships.DistinctBy(r => r.Verbal.Text.ToLower());
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
            return x.Match().Yield<bool>().With<IReferencer>(r => r.ReferesTo.Any(e =>
                                e.Text == y.Text ||
                                e.IsAliasFor(y) ||
                                e.IsSimilarTo(y))).Result();
        }
        private static bool CompareNounPhrasesOld(NounPhrase x, NounPhrase y) {
            var leftAsPro = x as IReferencer;
            var rightAsPro = y as IReferencer;
            var result = rightAsPro != null && x.Referencers.Contains(rightAsPro) || leftAsPro != null && y.Referencers.Contains(leftAsPro);

            if (!result) {
                result = x.Text == y.Text || x.IsAliasFor(y) || x.IsSimilarTo(y);
            }
            return result;
        }
    }

}
