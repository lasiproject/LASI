using LASI;
using LASI.Core;
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
        /// <param name="sources">The Documents to Join.</param>
        /// <returns>A Task&lt;IEnumerable&lt;RelationshipTuple&gt;&gt; corresponding to the intersection of the Documents to be joined .</returns>
        public async Task<IEnumerable<SvoRelationship>> GetCommonResultsAsnyc(IEnumerable<IReifiedTextual> sources) {
            var resultSets = await Task.WhenAll(
                new[] {
                    Task.Run(()=> GetCommonalitiesByVerbals(sources)),
                    Task.Run(()=> GetCommonalitiesByEntities(sources))
                });
            return resultSets.SelectMany(resultSet => resultSet).Distinct();
        }
        /// <summary>
        /// Builds and computes the intersection of the given Documents contained and returns the results as a sequence of Relationship instances.
        /// </summary>
        /// <param name="sources">The Documents to Join.</param>
        /// <returns>A Task&lt;IEnumerable&lt;RelationshipTuple&gt;&gt; corresponding to the intersection of the Documents to be joined .</returns>
        public IEnumerable<SvoRelationship> GetCommonResults(IEnumerable<IReifiedTextual> sources) {
            return GetCommonResultsAsnyc(sources).Result;
        }
        private async Task<IEnumerable<SvoRelationship>> GetCommonalitiesByEntities(IEnumerable<IReifiedTextual> sources) {
            await Task.Yield();
            var topNPsByDoc = from document in sources
                               .AsParallel()
                               .WithDegreeOfParallelism(Concurrency.Max)
                              select new { TopNounPhrases = GetTopNounPhrases(document), Document = document };

            await Task.Yield();
            var crossReferenced =
                from outer in topNPsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                from inner in topNPsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                where inner.Document != outer.Document
                from nounPhrase in outer.TopNounPhrases
                where inner.TopNounPhrases.Contains(nounPhrase, CompareNounPhrases)
                select nounPhrase;
            await Task.Yield();
            return from nounPhrase in crossReferenced.Distinct(CompareNounPhrases)
                   orderby nounPhrase.SubjectOf.Text
                   select new SvoRelationship(new AggregateEntity(nounPhrase), nounPhrase.SubjectOf, new AggregateEntity(nounPhrase.SubjectOf.DirectObjects), new AggregateEntity(nounPhrase.SubjectOf.IndirectObjects));
        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(IReifiedTextual source) {
            return source.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                       .OfNounPhrase()
                       .InSubjectRole()
                       .InObjectRole()
                       .Distinct(CompareNounPhrases)
                       .OrderBy(nounPhrase => nounPhrase.Weight);
        }
        private async Task<IEnumerable<SvoRelationship>> GetCommonalitiesByVerbals(IEnumerable<IReifiedTextual> sources) {
            var topVerbalsByDoc = await Task.WhenAll(sources.AsParallel().WithDegreeOfParallelism(Concurrency.Max).Select(GetTopVerbPhrasesAsync));
            var verbalCominalities = from verbals in topVerbalsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                     from verbal in verbals
                                     where (from verbs in topVerbalsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                            select verbs.Contains(verbal, (x, y) => x.Text == y.Text || x.IsSimilarTo(y)))
                                            .All(x => x)
                                     select verbal;
            var relationships = from verbal in verbalCominalities
                                let testPronouns = new Func<IEnumerable<IEntity>, AggregateEntity>(entities => new AggregateEntity(
                                    from s in entities
                                    let asPro = s as IReferencer
                                    select asPro != null ? asPro.RefersTo.Any() ? asPro.RefersTo : s : s))
                                let relationship = new SvoRelationship(testPronouns(verbal.Subjects), verbal, testPronouns(verbal.DirectObjects), testPronouns(verbal.IndirectObjects))
                                where relationship.Subject != null
                                orderby relationship.Verbal.Weight
                                select relationship;
            return relationships.DistinctBy(r => r.Verbal.Text.ToLower());
        }
        private async Task<ParallelQuery<VerbPhrase>> GetTopVerbPhrasesAsync(IReifiedTextual source) {
            return await Task.Run(() =>
                from verbPhrase in source.Phrases
                    .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                    .OfVerbPhrase()
                    .WithSubject().WithObject().Distinct((x, y) => x.IsSimilarTo(y))
                orderby verbPhrase.Weight + verbPhrase.Subjects.Sum(e => e.Weight) + verbPhrase.DirectObjects.Sum(e => e.Weight) + verbPhrase.IndirectObjects.Sum(e => e.Weight)
                select verbPhrase);
        }

        private static bool CompareNounPhrases(NounPhrase first, NounPhrase second) {
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
                return CompareNounPhrases(x, y);
            }
            public int GetHashCode(NounPhrase obj) {
                return obj.Text.GetHashCode();
            }
        }
        private static bool ReferencerTestCompare(NounPhrase x, NounPhrase y) {
            return x.Match().Yield<bool>().Case<IReferencer>(r => r.RefersTo.Any(e =>
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
