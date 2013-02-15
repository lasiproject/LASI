using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.FrequencyHeuristics
{
    public class PronounAwareEntityHeuristic : SingleDocumentHeuristic
    {
        public PronounAwareEntityHeuristic(Document toAnalyse, int maxResultsPerCategory)
            : base(toAnalyse, maxResultsPerCategory) {
        }

        public override Metric Analyse() {
            var subjects = from Entity in SourceMaterial.Phrases.GetNounPhrases()
                           let nounPhrase = Entity as NounPhrase
                           where nounPhrase != null
                           let SB = new
                           {
                               nounPhrase,
                               refs = nounPhrase.IndirectReferences
                           }
                           group SB by SB.nounPhrase.Text into lexGroups
                           from LG in lexGroups
                           select new
                           {
                               LG.nounPhrase,
                               Freq = (from PRN in LG.refs
                                       select PRN).Count()
                           } into FreqGroups
                           orderby FreqGroups.Freq
                           select FreqGroups;
            var entityResults = from E in subjects.Take(MaxResultsPerCategory)
                                select E.nounPhrase;
            return new Metric {
                MostSignificantEntities = entityResults,
                MostSignificantActions = from E in entityResults
                                         let Instances = E.IndirectReferences.Concat(new[] { E as IEntity })
                                         from IE in Instances
                                         from e in Instances

                                         select new[] { IE.SubjectOf, IE.IndirectObjectOf, IE.DirectObjectOf } into boundActionsGroup
                                         from A in boundActionsGroup
                                         select A
            };
        }



        public override async Task<Metric> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }
    }
}
