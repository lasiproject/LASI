﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;

using LASI.Algorithm.Thesauri;

namespace LASI.InteropLayer
{
    public class CrossDocumentJoiner
    {
        public event EventHandler<IEnumerable<NVNN>> JoinCompleted {
            add {
                onJoinCompletedEventHandler += value;
            }
            remove {
                onJoinCompletedEventHandler -= value;
            }
        }

        private EventHandler<IEnumerable<NVNN>> onJoinCompletedEventHandler;
        public CrossDocumentJoiner(IEnumerable<Document> documents) {
            Documents = documents;

        }
        public async Task<IEnumerable<NVNN>> JoinDocumentsAsnyc() {

            return await await Task.Factory.ContinueWhenAll(new[]{Task<IEnumerable<NVNN>>.Run(()=> GetCommonalitiesByVerbals()),
Task<IEnumerable<NVNN>>.Run(()=> GetCommonalitiesByBoundEntities())}, async tasks => {
    var results = new List<NVNN>();
    foreach (var t in tasks) {
        results.AddRange(await t);
    }
    return results.Distinct();
});

            //onJoinCompletedEventHandler.Invoke(new object(), commonalities);

        }
        //public void JoinDocuments() {
        //    Task.Run(async () => {
        //        var verbalCommonalities = await GetCommonalitiesByVerbals();
        //        var nounCommonalities = await GetCommonalitiesByBoundEntities();
        //        return verbalCommonalities.Concat(nounCommonalities).Distinct();

        //    }).ContinueWith(async result => onJoinCompletedEventHandler.Invoke(this, await result), TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(true);

        //}

        private async Task<IEnumerable<NVNN>> GetCommonalitiesByBoundEntities() {
            var topNPsByDoc = await Task.WhenAll(from doc in Documents.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                                 select GetTopNounPhrases(doc));
            var nounCommonalities = (from outerSet in topNPsByDoc.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     from np in outerSet
                                     from innerSet in topNPsByDoc
                                     where innerSet != outerSet
                                     where innerSet.Contains(np, (L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                                     select np).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R));
            var results = from n in nounCommonalities.InSubjectRole().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                          select new NVNN {
                              Subject = n,
                              Verbal = n.SubjectOf as VerbPhrase,
                              Direct = n.SubjectOf.DirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                              Indirect = n.SubjectOf.IndirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                              ViaPreposition = n.SubjectOf.ObjectOfThePreoposition as NounPhrase
                          } into result
                          group result by result.Subject.Text into resultGrouped
                          select resultGrouped.First() into result
                          orderby result.Subject.Weight
                          select result;
            return results.Distinct();
        }
        private async Task<IEnumerable<NVNN>> GetCommonalitiesByVerbals() {
            var topVerbalsByDoc = await Task.WhenAll(from doc in Documents.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                                     select GetTopVerbPhrases(doc));
            var verbalCominalities = from set in topVerbalsByDoc.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     from vp in set
                                     where (from verbs in topVerbalsByDoc
                                            select verbs.Contains(vp, (L, R) => L.Text == R.Text || R.IsSimilarTo(R)))
                                            .Aggregate(true, (product, result) => product &= result)
                                     select vp;
            return (from v in verbalCominalities.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                    select new NVNN {
                        Verbal = v,
                        Direct = v.DirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                        Indirect = v.IndirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                        Subject = v.Subjects.OfType<NounPhrase>().FirstOrDefault(),
                        ViaPreposition = v.ObjectOfThePreoposition as NounPhrase
                    } into result
                    group result by result.Verbal.Text into resultGrouped
                    select resultGrouped.First() into result
                    orderby result.Verbal.Weight
                    select result);
        }

        public async Task<IEnumerable<NounPhrase>> GetTopNounPhrases(Document document) {
            return await Task.Run(() => from distinctNP in
                                            (from np in document.GetEntities().GetPhrases().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax).GetNounPhrases()
                                             where np.SubjectOf != null || np.DirectObjectOf != null || np.IndirectObjectOf != null
                                             select np).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                                        orderby distinctNP.Weight
                                        select distinctNP);

        }
        public async Task<IEnumerable<VerbPhrase>> GetTopVerbPhrases(Document document) {
            return await Task.Run(() => {
                var vpsWithSubject = document.Phrases.GetVerbPhrases().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax).WithSubject();
                return from vp in vpsWithSubject.WithDirectObject().Concat(vpsWithSubject.WithIndirectObject()).Distinct()
                       orderby vp.Weight +
                       vp.Subjects.Sum(e => e.Weight) +
                       vp.DirectObjects.Sum(e => e.Weight) +
                       vp.IndirectObjects.Sum(e => e.Weight)
                       select vp as VerbPhrase;
            });
        }


        public IEnumerable<Document> Documents {
            get;
            protected set;
        }
        public class NVNN
        {
            private VerbPhrase verbial;

            public VerbPhrase Verbal {
                get {
                    return verbial;
                }
                set {
                    verbial = value;
                }
            }
            private NounPhrase subject;

            public NounPhrase Subject {
                get {
                    return subject;
                }
                set {
                    subject = value;
                    RelationshipWeight += subject != null ? subject.Weight : 0;
                }
            }
            private NounPhrase direct;

            public NounPhrase Direct {
                get {
                    return direct;
                }
                set {
                    direct = value;
                    RelationshipWeight += direct != null ? direct.Weight : 0;
                }
            }
            private NounPhrase indirect;

            public NounPhrase Indirect {
                get {
                    return indirect;
                }
                set {
                    indirect = value;
                    RelationshipWeight += indirect != null ? indirect.Weight : 0;
                }
            }
            private ILexical viaPreposition;

            public ILexical ViaPreposition {
                get {
                    return viaPreposition;
                }
                set {
                    viaPreposition = value;
                    RelationshipWeight += viaPreposition != null ? viaPreposition.Weight : 0;
                }
            }
            public decimal RelationshipWeight {
                get;
                protected set;
            }
            /// <summary>
            /// Returns a textual representation of the NpVpNpNpQuatruple.
            /// </summary>
            /// <returns>A textual representation of the NpVpNpNpQuatruple.</returns>
            public override string ToString() {
                var result = Subject.Text + Verbal.Text;
                if (Direct != null) {
                    result += Direct.Text;
                }
                if (Indirect != null) {
                    result += Indirect.Text;
                }
                return result;
            }
            public override int GetHashCode() {
                return 1;
            }
            public override bool Equals(object obj) {

                return this == obj as NVNN;

            }
            public static bool operator ==(NVNN lhs, NVNN rhs) {

                if ((lhs as object != null || rhs as object == null) || (lhs as object == null || rhs as object != null))
                    return false;
                else if (lhs as object == null && rhs as object == null)
                    return true;
                else {
                    bool result = lhs.Verbal.Text == rhs.Verbal.Text || lhs.Verbal.IsSimilarTo(rhs.Verbal);
                    result &= Comparisons<NounPhrase>.AliasOrSimilarity.Equals(lhs.Subject, rhs.Subject);
                    if (lhs.Direct != null && rhs.Direct != null) {
                        result &= Comparisons<NounPhrase>.AliasOrSimilarity.Equals(lhs.Direct, rhs.Direct);
                    } else if (lhs.Direct == null || rhs.Direct == null)
                        return false;
                    if (lhs.Indirect != null && rhs.Indirect != null) {
                        result &= Comparisons<NounPhrase>.AliasOrSimilarity.Equals(lhs.Indirect, rhs.Indirect);
                    } else if (lhs.Indirect == null || rhs.Indirect == null)
                        return false;
                    return result;
                }
            }

            public static bool operator !=(NVNN lhs, NVNN rhs) {
                return !(lhs == rhs);
            }

        }



    }

}
