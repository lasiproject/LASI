using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.Heuristics
{
    public class PronounAwareEntityHeuristic : SingleDocumentHeuristic
    {
        public PronounAwareEntityHeuristic(Document toAnalyse, int maxResultsPerCategory)
            : base(toAnalyse, maxResultsPerCategory) {
        }

        public override Metric Analyse() {
            var subjects = from Entity in SourceMaterial.Phrases.GetNounPhrases().AsParallel()


                           let SB = new
                           {
                               Entity,
                               refs = Entity.IndirectReferences
                           }
                           group SB by SB.Entity.Text into lexGroups
                           from LG in lexGroups
                           select new
                           {
                               LG.Entity,
                               Freq = (from PRN in LG.refs
                                       select PRN).Count()
                           } into FreqGroups
                           orderby FreqGroups.Freq
                           select FreqGroups;
            var entityResults = from E in subjects.Take(MaxResultsPerCategory).AsParallel()
                                select new CountedEntityResult {
                                    Count = E.Freq, Entity = E.Entity
                                };
            return new Metric {
                MostSignificantEntities = entityResults,
                MostSignificantActions = from E in entityResults
                                         let Instances = E.Entity.IndirectReferences.Concat(new[] { E.Entity as IEntity })
                                         from IE in Instances
                                         from e in Instances

                                         select new[] { IE.SubjectOf, IE.IndirectObjectOf, IE.DirectObjectOf } into boundActionsGroup
                                         from A in boundActionsGroup
                                         select new CountedActionResult {
                                             Count = boundActionsGroup.Count(), Action = A
                                         }
            };
        }



        public override async Task<Metric> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }
    }
}
