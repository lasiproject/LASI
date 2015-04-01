using LASI;
using LASI.Core;
using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using LASI.Core.Configuration;
using LASI.Core.Analysis.Heuristics.Support;

namespace LASI.Core.Analysis.Heuristics
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
        public async Task<IEnumerable<SvoRelationship>> GetCommonResultsAsnyc(IEnumerable<IReifiedTextual> sources)
        {
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
        public IEnumerable<SvoRelationship> GetCommonResults(IEnumerable<IReifiedTextual> sources)
        {
            return GetCommonResultsAsnyc(sources).Result;
        }
        private async Task<IEnumerable<SvoRelationship>> GetCommonalitiesByEntities(IEnumerable<IReifiedTextual> sources)
        {
            //await Task.Yield();
            var topNPsByDoc = from document in sources.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                              select new
                              {
                                  TopNounPhrases = GetTopNounPhrases(document),
                                  Document = document
                              };

            //await Task.Yield();
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
                   select new SvoRelationship(
                       subject: new AggregateEntity(nounPhrase),
                       verbal: nounPhrase.SubjectOf,
                       direct: new AggregateEntity(nounPhrase.SubjectOf.DirectObjects),
                       indirect: new AggregateEntity(nounPhrase.SubjectOf.IndirectObjects)
                    );
        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(IReifiedTextual source)
        {
            return source.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                       .OfNounPhrase()
                       .InSubjectRole()
                       .InObjectRole()
                       .Distinct(CompareNounPhrases)
                       .OrderBy(nounPhrase => nounPhrase.Weight);
        }
        private async Task<IEnumerable<SvoRelationship>> GetCommonalitiesByVerbals(IEnumerable<IReifiedTextual> sources)
        {
            var topVerbalsByDoc = await Task.WhenAll(sources.AsParallel().WithDegreeOfParallelism(Concurrency.Max).Select(GetTopVerbPhrasesAsync));
            var verbalCominalities = from verbals in topVerbalsByDoc.ToList().AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                     from verbal in verbals
                                     where topVerbalsByDoc.ToList()
                                        .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                        .All(verbs => verbs.Contains(verbal, (x, y) => x.Text == y.Text || x.IsSimilarTo(y)))
                                     select verbal;
            var relationships = from verbal in verbalCominalities
                                let testPronouns = new Func<IEnumerable<IEntity>, AggregateEntity>(entities => new AggregateEntity(
                                    from entity in entities
                                    select entity.Match().Yield<IEntity>()
                                        .When((IReferencer r) => r.RefersTo?.Any() ?? false)
                                        .Then((IReferencer r) => r.RefersTo)
                                        .Result(entity)))
                                let subject = testPronouns(verbal.Subjects)
                                where subject != null
                                orderby verbal.Weight
                                select new SvoRelationship(
                                    subject: subject,
                                    verbal: verbal,
                                    direct: testPronouns(verbal.DirectObjects),
                                    indirect: testPronouns(verbal.IndirectObjects));
            return relationships.DistinctBy(r => r.Verbal.Text.ToLower());
        }
        private async Task<ParallelQuery<VerbPhrase>> GetTopVerbPhrasesAsync(IReifiedTextual source)
        {
            return await Task.Run(() =>
                from verbPhrase in source.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                    .OfVerbPhrase()
                    .WithSubject()
                    .WithObject()
                    .Distinct((x, y) => x.IsSimilarTo(y))
                orderby verbPhrase.Weight + verbPhrase.Subjects.Concat(verbPhrase.DirectAndIndirectObjects).Sum(e => e.Weight)
                select verbPhrase);
        }

        private static bool CompareNounPhrases(NounPhrase first, NounPhrase second)
        {
            return ReferencerTestCompare(first, second) ||
                   ReferencerTestCompare(second, first) ||
                   first.Text == second.Text ||
                   first.IsAliasFor(second) ||
                   first.IsSimilarTo(second);
        }

        private static bool ReferencerTestCompare(NounPhrase x, NounPhrase y)
        {
            return x.Match().Case((IReferencer r) => r.RefersTo.Any(e =>
                               e.Text == y.Text ||
                               e.IsAliasFor(y) ||
                               e.IsSimilarTo(y))).Result();
        }
        private static bool CompareNounPhrasesOld(NounPhrase x, NounPhrase y)
        {
            var leftAsRef = x as IReferencer;
            var rightAsRef = y as IReferencer;

            return rightAsRef != null && x.Referencers.Contains(rightAsRef) ||
                leftAsRef != null && y.Referencers.Contains(leftAsRef) ||
                x.Text == y.Text ||
                x.IsAliasFor(y) ||
                x.IsSimilarTo(y);
        }
    }

}
