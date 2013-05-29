using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.PredefinedComparers;
using LASI.Algorithm.Thesauri;

namespace LASI.InteropLayer
{
    public class CrossDocumentJoiner
    {
        private System.Windows.Controls.ContentControl[] StatusMessageProviders;
        private System.Windows.Controls.ProgressBar ProgressBar;
        public CrossDocumentJoiner(IEnumerable<Document> documents, System.Windows.Controls.ProgressBar progressBar, params  System.Windows.Controls.ContentControl[] statusMessageProviders) {
            Documents = documents;
            StatusMessageProviders = statusMessageProviders;
            ProgressBar = progressBar;
        }
        public async Task<IEnumerable<NVNN>> JoinDocuments() {
            var verbalCommonalities = await GetCommonalitiesByVerbals();
            var nounCommonalities = await GetCommonalitiesByBoundEntities();
            return verbalCommonalities.Concat(nounCommonalities);

        }
        private async Task<IEnumerable<NVNN>> GetCommonalitiesByBoundEntities() {
            var topNPsByDoc = await Task.WhenAll(from doc in Documents
                                                 select GetTopNounPhrases(doc));
            var nounCommonalities = from outSet in topNPsByDoc
                                    from np in outSet
                                    where (from innerSet in topNPsByDoc
                                           select innerSet.Contains(np, NounPhraseComparers.Similarity)).Aggregate((product, result) => product &= result)
                                    select np;
            return from n in nounCommonalities.InSubjectRole()
                   select new NVNN {
                       Subject = n,
                       Verbal = n.SubjectOf as VerbPhrase,
                       Direct = n.SubjectOf.DirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                       Indirect = n.SubjectOf.IndirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                       ViaPreposition = n.SubjectOf.ObjectViaPreposition as NounPhrase
                   } into result
                   group result by result.Subject.Text into resultGrouped
                   select resultGrouped.First() into result
                   orderby result.Subject.Weight
                   select result;
        }
        private async Task<IEnumerable<NVNN>> GetCommonalitiesByVerbals() {
            var topVerbalsByDoc = await Task.WhenAll(from doc in Documents
                                                     select GetTopVerbPhrases(doc));
            var verbalCominalities = from set in topVerbalsByDoc
                                     from vp in set
                                     where (from s in topVerbalsByDoc
                                            select s.Contains(vp, new VPComparer())).Aggregate(true, (product, result) => product &= result)
                                     select vp;
            return (from v in verbalCominalities
                    select new NVNN {
                        Verbal = v,
                        Direct = v.DirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                        Indirect = v.IndirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                        Subject = v.BoundSubjects.OfType<NounPhrase>().FirstOrDefault(),
                        ViaPreposition = v.ObjectViaPreposition as NounPhrase
                    } into result
                    group result by result.Verbal.Text into resultGrouped
                    select resultGrouped.First() into result
                    orderby result.Verbal.Weight
                    select result);
        }

        public async Task<IEnumerable<NounPhrase>> GetTopNounPhrases(Document document) {
            return await Task.Run(() => from np in document.GetEntities().AsParallel().GetPhrases().GetNounPhrases()
                                        where np.SubjectOf != null || np.DirectObjectOf != null || np.IndirectObjectOf != null
                                        group np by np.ID into distinctNPs
                                        select distinctNPs.First() into topNP
                                        orderby topNP.Weight
                                        select topNP);

        }
        public async Task<IEnumerable<VerbPhrase>> GetTopVerbPhrases(Document document) {
            return await Task.Run(() => {
                var vpsWithSubject = document.Phrases.GetVerbPhrases().WithSubject();
                return from vp in vpsWithSubject.WithDirectObject().Concat(vpsWithSubject.WithIndirectObject()).Distinct()
                       orderby vp.BoundSubjects.OfType<NounPhrase>().Aggregate(0m, (sum, e) => sum += e.Weight) + vp.DirectObjects.OfType<NounPhrase>().Aggregate(0m, (sum, e) => sum += e.Weight) + vp.IndirectObjects.OfType<NounPhrase>().Aggregate(0m, (sum, e) => sum += e.Weight)
                       select vp as VerbPhrase;
            });
        }
        private void UpdateProgressDisplay(string statusMessage) {
            foreach (var smp in StatusMessageProviders) {

                smp.Content = statusMessage;
            }
            ProgressBar.Value += (float)Documents.Count() / 100;
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
            /// Returns entity textual representation of the NpVpNpNpQuatruple.
            /// </summary>
            /// <returns>entity textual representation of the NpVpNpNpQuatruple.</returns>
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
                if (obj != null)
                    return this == obj as NVNN;
                else
                    return false;
            }
            public static bool operator ==(NVNN lhs, NVNN rhs) {

                if ((lhs as object != null || rhs as object == null) || (lhs as object == null || rhs as object != null))
                    return false;
                else if (lhs as object == null && rhs as object == null)
                    return true;
                else {
                    bool result = lhs.Verbal.Text == rhs.Verbal.Text || lhs.Verbal.IsSimilarTo(rhs.Verbal);
                    result &= NounPhraseComparers.AliasOrSimilarity.Equals(lhs.Subject, rhs.Subject);
                    if (lhs.Direct != null && rhs.Direct != null) {
                        result &= NounPhraseComparers.AliasOrSimilarity.Equals(lhs.Direct, rhs.Direct);
                    }
                    else if (lhs.Direct == null || rhs.Direct == null)
                        return false;
                    if (lhs.Indirect != null && rhs.Indirect != null) {
                        result &= NounPhraseComparers.AliasOrSimilarity.Equals(lhs.Indirect, rhs.Indirect);
                    }
                    else if (lhs.Indirect == null || rhs.Indirect == null)
                        return false;
                    return result;
                }
            }

            public static bool operator !=(NVNN lhs, NVNN rhs) {
                return !(lhs == rhs);
            }

        }

        public class VPComparer : IEqualityComparer<ITransitiveVerbal>
        {
            public bool Equals(ITransitiveVerbal x, ITransitiveVerbal y) {
                var xO = x as object;
                var yO = y as object;
                if (xO == null || yO == null)
                    return xO == null && yO == null;
                else
                    return x.Text == y.Text || (x as VerbPhrase != null && y as VerbPhrase != null) ? (x as VerbPhrase).IsSimilarTo(y as VerbPhrase) : false;
            }

            public int GetHashCode(ITransitiveVerbal obj) {
                return obj != null ? 1 : 0;
            }
        }

    }

}
