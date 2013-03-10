using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm.Heuristics
{
    /// <summary>
    /// a analyser which attempts to return the significant constructs in a document based on the Pronoun bolsterd frequency of the Entities therein.
    /// </summary>
    public class DocumentAnalyzer : LASI.Algorithm.Heuristics.IDocumentAnalyzer
    {

        /// <summary>
        /// Initializes a new instance of the PronounAwareEntityHeuristic class.
        /// </summary>
        /// <param name="document">The ParentDocument object which the Heuristic will assess.</param>
        /// <param name="maxResults">The maximum number of results to retain in each results group.</param>
        /// <remarks>Providing large values for maxResultsPerCategory may drastically impact performance.</remarks>
        public DocumentAnalyzer(Document document, int maxResults, PronounMode pronounMode) {
            SourceDocument = document;
            MaxResults = maxResults;
            PronounMode = pronounMode;

        }
        /// <summary>
        /// Analyses the ParentDocument object referenced by this instance and returns a collection of statistical results based primarily on the Pronoun bolsterd frequency of the Entities therein.
        /// </summary>
        /// <returns>An object containing the most significant Entities and Actions occuring in the document, according to the Heuristic'd methodology.</returns>
        public Metric Analyse() {
            var subjects = from Entity in SourceDocument.Phrases.GetNounPhrases().AsParallel()
                           let SB = new
                           {
                               Entity,
                               refs = Entity.BoundPronouns
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
            var entityResults = from E in subjects.Take(MaxResults).AsParallel()
                                select new
                                {
                                    Count = E.Freq,
                                    Entity = E.Entity
                                };
            throw new NotImplementedException();


        }


        /// <summary>
        /// Analyses the ParentDocument object referenced by this instance and returns a collection of statistical results based primarily on the Pronoun bolsterd frequency of the Entities therein.
        /// The assessment is performed asynchronously on a background thread.
        /// </summary>
        /// <returns>a Task of Metric object which, results in a task which contains the most significant Entities and Actions occuring in the document, according to the Heuristic'd methodology.</returns>
        public async Task<Metric> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }

        public int MaxResults {
            get;
            set;
        }


        public Document SourceDocument {
            get;
            set;
        }
        public PronounMode PronounMode {
            get;
            set;
        }
    }
}
