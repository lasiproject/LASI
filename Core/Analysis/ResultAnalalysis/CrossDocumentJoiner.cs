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
                    foreach (var task in tasks) { results.AddRange(await task); }
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
            var topNPsByDoc = from document in documents
                               .AsParallel()
                               .WithDegreeOfParallelism(Concurrency.Max)
                              select new { TopNounPhrases = GetTopNounPhrases(document), Document = document };

            await Task.Yield();
            var crossReferenced =
                from outer in topNPsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                from inner in topNPsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                where inner.Document != outer.Document
                from nounPhrase in outer.TopNounPhrases
                where inner.TopNounPhrases.Contains(nounPhrase, CompareNps)
                select nounPhrase;
            await Task.Yield();
            return
                from nounPhrase in crossReferenced.Distinct(CompareNps)
                orderby nounPhrase.SubjectOf.Text
                select new SvoRelationship(new AggregateEntity(nounPhrase), nounPhrase.SubjectOf, new AggregateEntity(nounPhrase.SubjectOf.DirectObjects), new AggregateEntity(nounPhrase.SubjectOf.IndirectObjects));
        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(Document document) {
            return document.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
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
                                entities => new AggregateEntity(from s in entities let asPro = s as IReferencer select asPro != null ? asPro.RefersTo.Any() ? asPro.RefersTo : s : s))
                                let relationship = new SvoRelationship(testPronouns(verbal.Subjects), verbal, testPronouns(verbal.DirectObjects), testPronouns(verbal.IndirectObjects))
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

        private static bool CompareNps(NounPhrase first, NounPhrase second) {
            return
                ReferencerTestCompare(first, second) ||
                ReferencerTestCompare(second, first) ||
                first.Text == second.Text ||
                first.IsAliasFor(second) || first.IsSimilarTo(second);
        }
        private class NounPhraseComparer : IEqualityComparer<NounPhrase>
        {
            private NounPhraseComparer() { }
            private static readonly NounPhraseComparer comparer = new NounPhraseComparer();
            public static NounPhraseComparer Instance { get { return comparer; } }
            public bool Equals(NounPhrase x, NounPhrase y) {
                return CompareNps(x, y);
            }
            public int GetHashCode(NounPhrase obj) {
                return obj.Text.GetHashCode();
            }
        }
        private static bool ReferencerTestCompare(NounPhrase x, NounPhrase y) {
            return x.Match().Yield<bool>().With<IReferencer>(r => r.RefersTo.Any(e =>
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
