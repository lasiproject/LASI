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
        private System.Windows.Controls.ContentControl[] StatusMessageProviders;
        private System.Windows.Controls.ProgressBar ProgressBar;
        public CrossDocumentJoiner(IEnumerable<Document> documents, System.Windows.Controls.ProgressBar progressBar, params  System.Windows.Controls.ContentControl[] statusMessageProviders) {
            Documents = documents;
            StatusMessageProviders = statusMessageProviders;
            ProgressBar = progressBar;
        }
        public async Task<IEnumerable<NVNN>> JoinDocuments() {
            var allSets = await Task.WhenAll(from doc in Documents
                                             select GetTopVerbials(doc));
            IEnumerable<VerbPhrase> cominalities = from set in allSets
                                                   from vp in set
                                                   where (from s in allSets
                                                          select s.Contains(vp, new VPComparer())).Aggregate(true, (product, result) => product &= result)
                                                   select vp;
            return from c in cominalities
                   select new NVNN
                   {
                       Verbial = c,
                       Direct = c.DirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                       Indirect = c.IndirectObjects.OfType<NounPhrase>().FirstOrDefault(),
                       Subject = c.BoundSubjects.OfType<NounPhrase>().FirstOrDefault(),
                   };

        }

        public class VPComparer : IEqualityComparer<VerbPhrase>
        {
            public bool Equals(VerbPhrase x, VerbPhrase y) {
                return x.Text == y.Text || x.IsSimilarTo(y);
            }

            public int GetHashCode(VerbPhrase obj) {
                return obj.GetHashCode();
            }
        }

        public class NVNN
        {
            private VerbPhrase verbial;

            public VerbPhrase Verbial {
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
            public decimal RelationshipWeight {
                get;
                protected set;
            }
            /// <summary>
            /// Returns a textual representation of the NpVpNpNpQuatruple.
            /// </summary>
            /// <returns>a textual representation of the NpVpNpNpQuatruple.</returns>
            public override string ToString() {
                var result = Subject.Text + Verbial.Text;
                if (Direct != null) {
                    result += Direct.Text;
                }
                if (Indirect != null) {
                    result += Indirect.Text;
                }
                return result;
            }
            public override int GetHashCode() {
                return base.GetHashCode();
            }
            public override bool Equals(object obj) {
                return this == obj as NVNN;
            }
            public static bool operator ==(NVNN lhs, NVNN rhs) {
                return lhs.ToString() == rhs.ToString() ||
                     lhs.Subject.IsSimilarTo(rhs.Subject) && lhs.Verbial.IsSimilarTo(rhs.Verbial);
            }
            public static bool operator !=(NVNN lhs, NVNN rhs) {
                return !(lhs == rhs);
            }

        }

        public async Task<IEnumerable<VerbPhrase>> GetTopVerbials(Document document) {
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
            ProgressBar.Value += (float) Documents.Count() / 100;
        }

        public IEnumerable<Document> Documents {
            get;
            protected set;
        }
    }

}
