﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;

using LASI.Algorithm.LexicalLookup;

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
        /// <returns>A Task of IEnumerable of RelationshipTuple corresponding to the intersection of the Documents to be joined .</returns>
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
            var topNPsByDoc = from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                              select GetTopNounPhrases(doc);
            var nounCommonalities = (from outerSet in topNPsByDoc
                                         .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     from np in outerSet
                                     from innerSet in topNPsByDoc
                                     where innerSet != outerSet
                                     where innerSet.Contains(np, (L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                                     select np).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R));
            var results = from n in nounCommonalities
                              .InSubjectRole()
                              .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                          select new RelationshipTuple {
                              Subject = n,
                              Verbal = n.SubjectOf,
                              Direct = n.SubjectOf.DirectObjects.FirstOrDefault(),
                              Indirect = n.SubjectOf.IndirectObjects.FirstOrDefault(),
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
            var topVerbalsByDoc = await Task.WhenAll(from doc in documents.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                                     select GetTopVerbPhrasesAsync(doc));
            var verbalCominalities = from verbPhraseSet in topVerbalsByDoc.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     from vp in verbPhraseSet
                                     where (from verbs in topVerbalsByDoc
                                            select verbs.Contains(vp, (L, R) => L.Text == R.Text || R.IsSimilarTo(R)))
                                            .Aggregate(true, (product, result) => product &= result)
                                     select vp;
            return (from v in verbalCominalities
                    select new RelationshipTuple {
                        Verbal = v,
                        Direct = v.DirectObjects
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).EntityRefererredTo).FirstOrDefault(),
                        Indirect = v.IndirectObjects
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).EntityRefererredTo).FirstOrDefault(),
                        Subject = v.Subjects
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).EntityRefererredTo).FirstOrDefault(),
                        Prepositional = v.ObjectOfThePreoposition
                    } into result
                    where result.Subject != null
                    group result by result.Verbal.Text into resultGrouped
                    select resultGrouped.First() into result
                    orderby result.Verbal.Weight
                    select result);
        }

        private async Task<IEnumerable<NounPhrase>> GetTopNounPhrasesAsync(Document document) {
            return await Task.Run(() => from distinctNP in
                                            (from np in document.Phrases
                                                 .GetNounPhrases()
                                                 .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                             where np.SubjectOf != null && (np.DirectObjectOf != null || np.IndirectObjectOf != null)
                                             select np).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                                        orderby distinctNP.Weight
                                        select distinctNP);

        }
        private IEnumerable<NounPhrase> GetTopNounPhrases(Document document) {
            return from distinctNP in
                       (from np in document.Phrases
                            .GetNounPhrases()
                            .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                            .InSubjectRole()
                        where (np.DirectObjectOf != null || np.IndirectObjectOf != null)
                        select np
                        ).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                   orderby distinctNP.Weight
                   select distinctNP;
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
