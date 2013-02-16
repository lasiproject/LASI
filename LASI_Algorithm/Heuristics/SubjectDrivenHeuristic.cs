using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.Heuristics
{
    public class SubjectDrivenHeuristic : SingleDocumentHeuristic
    {
        public SubjectDrivenHeuristic(Document toAnalyse, int maxResultsPerCategory)
            : base(toAnalyse, maxResultsPerCategory) {
        }

        public override Metric Analyse() {
            var subjects = from Entity in SourceMaterial.Phrases.GetNounPhrases()
                           where Entity.SubjectOf != null

                           let SV = new
                           {
                               Entity,
                               Entity.SubjectOf
                           }
                           group SV by SV.Entity.Text into SVG
                           orderby SVG.Count()
                           select new
                           {
                               SV = SVG.First(),
                               CNT = SVG.Count()
                           };
            return new Metric {
                MostSignificantEntities = from S in subjects.Take(MaxResultsPerCategory)
                                          select new CountedEntityResult {
                                              Count = S.CNT, Entity = S.SV.Entity
                                          },
                MostSignificantActions = from A in subjects
                                         group A by A.SV.SubjectOf.Text into actionGroup
                                         from a in actionGroup
                                         select new CountedActionResult {
                                             Action = a.SV.SubjectOf, Count = actionGroup.Count()
                                         }

            };
        }

        public async override Task<Metric> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }


    }
}
