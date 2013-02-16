using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.Heuristics
{
    /// <summary>
    /// A analyser which attempts to return the significant constructs in a document based on the Pronoun bolsterd frequency of the Entities therein.
    /// </summary>
    public class PronounAwareEntityHeuristic : SingleDocumentHeuristic
    {
        /// <summary>
        /// Initializes a new instance of the PronounAwareEntityHeuristic class.
        /// </summary>
        /// <param name="toAnalyse">The Document object which the Heuristic will assess.</param>
        /// <param name="maxResultsPerCategory">The maximum number of results to retain in each results group.</param>
        /// <remarks>Providing large values for maxResultsPerCategory may drastically impact performance.</remarks>
        public PronounAwareEntityHeuristic(Document toAnalyse, int maxResultsPerCategory)
            : base(toAnalyse, maxResultsPerCategory) {
        }
        /// <summary>
        /// Analyses the Document object referenced by this instance and returns a collection of statistical results based primarily on the Pronoun bolsterd frequency of the Entities therein.
        /// </summary>
        /// <returns>An object containing the most significant Entities and Actions occuring in the document, according to the Heuristic's methodology.</returns>
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


        /// <summary>
        /// Analyses the Document object referenced by this instance and returns a collection of statistical results based primarily on the Pronoun bolsterd frequency of the Entities therein.
        /// The assessment is performed asynchronously on a background thread.
        /// </summary>
        /// <returns>A Task of Metric object which, results in a task which contains the most significant Entities and Actions occuring in the document, according to the Heuristic's methodology.</returns>
        public override async Task<Metric> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }
    }
}
