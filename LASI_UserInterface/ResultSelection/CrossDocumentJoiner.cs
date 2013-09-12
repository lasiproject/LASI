using LASI;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.LexicalLookup;
using LASI.Algorithm.Aliasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.UserInterface
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
        internal async Task<IEnumerable<RelationshipTuple>> JoinDocumentsAsnyc(IEnumerable<Document> documents) {

            return await await Task.Factory.ContinueWhenAll(
                new[] {  
                    Task.Run(()=> GetCommonalitiesByVerbals(documents)),
                    Task.Run(()=> GetCommonalitiesByEntities(documents))
                },
                async tasks => {
                    var results = new List<RelationshipTuple>();
                    foreach (var t in tasks) {
                        results.AddRange(await t);
                    }
                    return results.Distinct();
                }
            );
        }

        private async Task<IEnumerable<RelationshipTuple>> GetCommonalitiesByEntities(IEnumerable<Document> documents) {
            var topNPsByDoc = from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                              select GetTopNounPhrases(doc);
            var nounCommonalities = (from outerSet in topNPsByDoc
                                         .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                     from np in outerSet
                                     from innerSet in topNPsByDoc
                                     where innerSet != outerSet
                                     where innerSet.Contains(np, (left, right) => {
                                         var result = left.Text == right.Text ||
                                             left.IsAliasFor(right) ||
                                             left.IsSimilarTo(right);
                                         if (!result) {
                                             var leftAsPro = left as IPronoun;
                                             var rightAsPro = right as IPronoun;
                                             result = rightAsPro != null &&
                                                 left.BoundPronouns.Contains(rightAsPro) ||
                                                 leftAsPro != null &&
                                                 right.BoundPronouns.Contains(leftAsPro);
                                         }
                                         return result;
                                     })
                                     select np).Distinct((left, right) => {
                                         var result = left.Text == right.Text ||
                                             left.IsAliasFor(right) ||
                                             left.IsSimilarTo(right);
                                         if (!result) {
                                             var leftAsPro = left as IPronoun;
                                             var rightAsPro = right as IPronoun;
                                             result = rightAsPro != null &&
                                                 left.BoundPronouns.Contains(rightAsPro) ||
                                                 leftAsPro != null &&
                                                 right.BoundPronouns.Contains(leftAsPro);
                                         }
                                         return result;
                                     });
            var results = from n in nounCommonalities
                              .InSubjectRole()
                              .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                          select new RelationshipTuple {
                              Subject = new AggregateEntity(new[] { n }),
                              Verbal = n.SubjectOf,
                              Direct = new AggregateEntity(n.SubjectOf.DirectObjects),
                              Indirect = new AggregateEntity(n.SubjectOf.IndirectObjects),
                              Prepositional = n.SubjectOf.ObjectOfThePreoposition
                          } into result
                          group result by result.Subject.Text into resultGrouped
                          select resultGrouped.First() into result
                          orderby result.Subject.Weight
                          select result;
            await Task.Yield();
            return results.Distinct();

        }
        private async Task<IEnumerable<RelationshipTuple>> GetCommonalitiesByVerbals(IEnumerable<Document> documents) {
            var topVerbalsByDoc = await Task.WhenAll(from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                                     select GetTopVerbPhrasesAsync(doc));
            var verbalCominalities = from verbPhraseSet in topVerbalsByDoc.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                     from vp in verbPhraseSet
                                     where (from verbs in topVerbalsByDoc
                                            select verbs.Contains(vp, (L, R) => L.Text == R.Text || R.IsSimilarTo(R)))
                                            .Aggregate(true, (product, result) => product &= result)
                                     select vp;
            return (from v in verbalCominalities
                    select new RelationshipTuple {
                        Verbal = v,
                        Subject = new AggregateEntity(v.Subjects
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).RefersTo)),
                        Direct = new AggregateEntity(v.DirectObjects
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).RefersTo)),
                        Indirect = new AggregateEntity(v.IndirectObjects
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).RefersTo)),
                        Prepositional = v.ObjectOfThePreoposition
                    } into result
                    where result.Subject != null
                    group result by result.Verbal.Text into groupedResult
                    select groupedResult.First() into result
                    orderby result.Verbal.Weight
                    select result);
        }

        private async Task<IEnumerable<NounPhrase>> GetTopNounPhrasesAsync(Document document) {
            return await Task.Run(() => document.Phrases
                .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .GetNounPhrases()
                .InSubjectRole()
                .InObjectRole()
                .Distinct((left, right) => {
                    var result = left.Text == right.Text || left.IsAliasFor(right) || left.IsSimilarTo(right);
                    if (!result) {
                        var leftAsPro = left as IPronoun;
                        var rightAsPro = right as IPronoun;
                        result = rightAsPro != null && left.BoundPronouns.Contains(rightAsPro) || leftAsPro != null && right.BoundPronouns.Contains(leftAsPro);
                    }
                    return result;
                })
                .OrderBy(dnp => dnp.Weight));

        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(Document document) {
            return document.Phrases
                       .AsParallel().WithDegreeOfParallelism(Concurrency.Max).GetNounPhrases()
                       .InSubjectRole()
                       .InObjectRole()
                       .Distinct((left, right) => {
                           var result = left.Text == right.Text || left.IsAliasFor(right) || left.IsSimilarTo(right);
                           if (!result) {
                               var leftAsPro = left as IPronoun;
                               var rightAsPro = right as IPronoun;
                               result = rightAsPro != null && left.BoundPronouns.Contains(rightAsPro) || leftAsPro != null && right.BoundPronouns.Contains(leftAsPro);
                           }
                           return result;
                       })
                       .OrderBy(dnp => dnp.Weight);
        }
        private async Task<ParallelQuery<VerbPhrase>> GetTopVerbPhrasesAsync(Document document) {
            return await Task.Run(() => {
                var vpsWithSubject = document.Phrases.AsParallel().GetVerbPhrases().WithSubject();
                return from vp in vpsWithSubject.WithDirectObject().Concat(vpsWithSubject.WithIndirectObject()).Distinct((vLeft, vRight) => vLeft.IsSimilarTo(vRight))
                       orderby vp.Weight + vp.Subjects.Sum(e => e.Weight) + vp.DirectObjects.Sum(e => e.Weight) + vp.IndirectObjects.Sum(e => e.Weight)
                       select vp;
            });
        }
    }

}
