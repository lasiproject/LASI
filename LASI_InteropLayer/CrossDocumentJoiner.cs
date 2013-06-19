using System;
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


        public CrossDocumentJoiner(IEnumerable<Document> documents) {
            Documents = documents;

        }
        public async Task<IEnumerable<NVNN>> JoinDocumentsAsnyc() {

            return await await Task.Factory.ContinueWhenAll(
                new[]{  Task.Run(()=> GetCommonalitiesByVerbals()),
                        Task.Run(()=> GetCommonalitiesByBoundEntities())}, async tasks => {
                            var results = new List<NVNN>();
                            foreach (var t in tasks) {
                                results.AddRange(await t);
                            }
                            return results.Distinct();
                        });



        }

        private async Task<IEnumerable<NVNN>> GetCommonalitiesByBoundEntities() {
            var topNPsByDoc = from doc in Documents.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
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
                          select new NVNN {
                              Subject = n,
                              Verbal = n.SubjectOf as VerbPhrase,
                              Direct = n.SubjectOf
                                  .DirectObjects
                                  .OfType<NounPhrase>()
                                  .FirstOrDefault(),
                              Indirect = n.SubjectOf
                                  .IndirectObjects
                                  .OfType<NounPhrase>()
                                  .FirstOrDefault(),
                              ViaPreposition = n.SubjectOf.ObjectOfThePreoposition as NounPhrase
                          } into result
                          group result by result.Subject.Text into resultGrouped
                          select resultGrouped.First() into result
                          orderby result.Subject.Weight
                          select result;
            await Task.Yield();
            return results.Distinct();

        }
        private async Task<IEnumerable<NVNN>> GetCommonalitiesByVerbals() {
            var topVerbalsByDoc = await Task.WhenAll(from doc in Documents.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                                     select GetTopVerbPhrases(doc));
            var verbalCominalities = from verbPhraseSet in topVerbalsByDoc.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     from vp in verbPhraseSet
                                     where (from verbs in topVerbalsByDoc
                                            select verbs.Contains(vp, (L, R) => L.Text == R.Text || R.IsSimilarTo(R)))
                                            .Aggregate(true, (product, result) => product &= result)
                                     select vp;
            return (from v in verbalCominalities.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                    select new NVNN {
                        Verbal = v,
                        Direct = v.DirectObjects
                            .OfType<NounPhrase>()
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).BoundEntity)
                            .FirstOrDefault(),
                        Indirect = v.IndirectObjects
                            .OfType<NounPhrase>()
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).BoundEntity)
                            .FirstOrDefault(),
                        Subject = v.Subjects
                            .OfType<NounPhrase>()
                            .Select(s => (s as IPronoun) == null ? s : (s as IPronoun).BoundEntity)
                            .FirstOrDefault(),
                        ViaPreposition = v.ObjectOfThePreoposition as NounPhrase
                    } into result
                    where result.Subject != null
                    group result by result.Verbal.Text into resultGrouped
                    select resultGrouped.First() into result
                    orderby result.Verbal.Weight
                    select result);
        }

        public async Task<IEnumerable<NounPhrase>> GetTopNounPhrasesAsync(Document document) {
            return await Task.Run(() => from distinctNP in
                                            (from np in document
                                                 .GetEntities().GetPhrases()
                                                 .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                                 .GetNounPhrases()
                                             where np.SubjectOf != null && (np.DirectObjectOf != null || np.IndirectObjectOf != null)
                                             select np).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                                        orderby distinctNP.Weight
                                        select distinctNP);

        }
        public IEnumerable<NounPhrase> GetTopNounPhrases(Document document) {
            return from distinctNP in
                       (from np in document
                            .GetEntities().GetPhrases()
                            .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                            .GetNounPhrases()
                        where np.SubjectOf != null && (np.DirectObjectOf != null || np.IndirectObjectOf != null)
                        select np
                        ).Distinct((L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R))
                   orderby distinctNP.Weight
                   select distinctNP;

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
            private IEntity subject;

            public IEntity Subject {
                get {
                    return subject;
                }
                set {
                    subject = value;
                    RelationshipWeight += subject != null ? subject.Weight : 0;
                }
            }
            private IEntity direct;

            public IEntity Direct {
                get {
                    return direct;
                }
                set {
                    direct = value;
                    RelationshipWeight += direct != null ? direct.Weight : 0;
                }
            }
            private IEntity indirect;

            public IEntity Indirect {
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
            public double RelationshipWeight {
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
                    result &= Comparers<IEntity>.AliasOrSimilarity.Equals(lhs.Subject, rhs.Subject);
                    if (lhs.Direct != null && rhs.Direct != null) {
                        result &= Comparers<IEntity>.AliasOrSimilarity.Equals(lhs.Direct, rhs.Direct);
                    } else if (lhs.Direct == null || rhs.Direct == null)
                        return false;
                    if (lhs.Indirect != null && rhs.Indirect != null) {
                        result &= Comparers<IEntity>.AliasOrSimilarity.Equals(lhs.Indirect, rhs.Indirect);
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
