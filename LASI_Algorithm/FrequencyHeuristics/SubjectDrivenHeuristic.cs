using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.FrequencyHeuristics
{
    public class SubjectDrivenHeuristic : SingleDocumentHeuristic
    {
        public SubjectDrivenHeuristic(Document toAnalyse, int maxResultsPerCategory)
            : base(toAnalyse, maxResultsPerCategory) {
        }

        public override Metric Analyse() {
            var subjects = from Entity in SourceMaterial.Phrases.GetNounPhrases()
                           let nounPhrase = Entity as NounPhrase
                           where nounPhrase != null && nounPhrase.SubjectOf != null

                           let SV = new
                           {
                               nounPhrase,
                               nounPhrase.SubjectOf
                           }
                           group SV by SV.nounPhrase.Text into SVG
                           orderby SVG.Count()
                           select SVG.First();
            return new Metric {
                MostSignificantEntities = from S in subjects.Take(MaxResultsPerCategory)
                                          select S.nounPhrase,
                MostSignificantActions = from A in subjects
                                         select A.SubjectOf
            };
        }

        public async override Task<Metric> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }


    }
}
