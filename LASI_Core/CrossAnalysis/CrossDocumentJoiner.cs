using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using LASI.Core.ComparativeHeuristics;
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
        /// Asynchrnously builds computes the intersection of the given Documents contained and returns the results as a sequence of RelationshipTuple instances.
        /// </summary>
        /// <param name="documents">The Documents to Join.</param>
        /// <returns>A Task&lt;IEnumerable&lt;RelationshipTuple&gt;&gt; corresponding to the intersection of the Documents to be joined .</returns>
        public async Task<IEnumerable<Relationship>> JoinDocumentsAsnyc(IEnumerable<Document> documents) {
            return await await Task.Factory.ContinueWhenAll(
                new[] {  
                    Task.Run(()=> GetCommonalitiesByVerbals(documents)),
                    Task.Run(()=> GetCommonalitiesByEntities(documents))
                },
                async tasks => {
                    var results = new List<Relationship>();
                    foreach (var t in tasks) {
                        results.AddRange(await t);
                    }
                    return results.Distinct();
                }
            );
        }
        private async Task<IEnumerable<Relationship>> GetCommonalitiesByEntities(IEnumerable<Document> documents) {
            var topNPsByDoc = from doc in documents
                               .AsParallel()
                               .WithDegreeOfParallelism(Concurrency.Max)
                              select new { topNPs = GetTopNounPhrases(doc), doc };

            var crossReferenced = from outer in topNPsByDoc
                                  from inner in topNPsByDoc
                                  where inner.doc != outer.doc
                                  from n in outer.topNPs
                                  where inner.topNPs.Contains(n, CompareNounPhrases)
                                  select n;
            var results = from n in crossReferenced.Distinct(CompareNounPhrases)
                          select new Relationship {
                              Verbal = n.SubjectOf,
                              Subject = new AggregateEntity(new[] { n }),
                              Direct = new AggregateEntity(n.SubjectOf.DirectObjects),
                              Indirect = new AggregateEntity(n.SubjectOf.IndirectObjects),
                              Prepositional = n.SubjectOf.ObjectOfThePreoposition
                          } into result
                          orderby result.Subject.Text
                          select result;
            await Task.Yield();
            return results.Distinct();
        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(Document document) {
            return document.Phrases
                       .AsParallel()
                       .WithDegreeOfParallelism(Concurrency.Max)
                       .OfNounPhrase()
                       .InSubjectRole()
                       .InObjectRole()
                       .Distinct(CompareNounPhrases)
                       .OrderBy(np => np.Weight);
        }
        private async Task<IEnumerable<Relationship>> GetCommonalitiesByVerbals(IEnumerable<Document> documents) {
            var topVerbalsByDoc = await Task.WhenAll(from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                                     select GetTopVerbPhrasesAsync(doc));
            var verbalCominalities = from topVPs in topVerbalsByDoc
                                     from verbal in topVPs
                                     where (
                                        from verbs in topVerbalsByDoc
                                        select verbs.Contains(verbal, (x, y) => x.Text == y.Text || y.IsSimilarTo(y)))
                                     .Aggregate(true, (product, result) => product &= result)
                                     select verbal;
            return from verbal in verbalCominalities
                   let testPronouns = new Func<IEnumerable<IEntity>, AggregateEntity>(
                   entities => new AggregateEntity(from s in entities let asPro = s as IReferencer select asPro != null ? asPro.Referent.Any() ? asPro.Referent : s : s))
                   select new Relationship {
                       Verbal = verbal,
                       Subject = testPronouns(verbal.Subjects),
                       Direct = testPronouns(verbal.DirectObjects),
                       Indirect = testPronouns(verbal.IndirectObjects),
                       Prepositional = verbal.ObjectOfThePreoposition
                   } into result
                   where result.Subject.Any()
                   orderby result.Verbal.Weight
                   group result by result.Verbal.Text into groupedResult
                   select groupedResult.First();
        }
        private async Task<ParallelQuery<VerbPhrase>> GetTopVerbPhrasesAsync(Document document) {
            return await Task.Run(() => {
                var vpsWithSubject =
                    document.Phrases
                    .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                    .OfVerbPhrase()
                    .WithSubject();
                return from vp in vpsWithSubject.WithObject().Distinct((vLeft, vRight) => vLeft.IsSimilarTo(vRight))
                       orderby vp.Weight + vp.Subjects.Sum(e => e.Weight) + vp.DirectObjects.Sum(e => e.Weight) + vp.IndirectObjects.Sum(e => e.Weight)
                       select vp;
            });
        }

        private static bool CompareNounPhrases(NounPhrase x, NounPhrase y) {
            return
                ReferencerTestCompare(x, y) ||
                ReferencerTestCompare(y, x) ||
                x.Text == y.Text ||
                x.IsAliasFor(y) || x.IsSimilarTo(y);
        }

        private static bool ReferencerTestCompare(NounPhrase x, NounPhrase y) {
            return x.Match().Yield<bool>()
                            .With<IReferencer>(pro => pro.Referent.Any(rX =>
                                rX.Text == y.Text ||
                                rX.IsAliasFor(y) ||
                                rX.IsSimilarTo(y))).Result();
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
