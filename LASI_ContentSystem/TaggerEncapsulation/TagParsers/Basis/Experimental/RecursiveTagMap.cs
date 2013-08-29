using LASI.ContentSystem.TaggerEncapsulation.TagParsers.Experiment.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem.TaggerEncapsulation.TagParsers.Basis.Experimental
{
    class RecursiveTagMap
    {
        #region Properties
        /// <summary>
        /// Returns the NodeTeir value corresponding to the indexing tag string.
        /// </summary>
        /// <param name="tag">The tag whose corresponding NodeTeir will be returned.</param>
        /// <returns>The NodeTeir value corresponding to the indexing tag string.</returns>
        public NodeTeir this[string tag] { get { return map[tag]; } }

        #endregion

        #region Fields
        internal static readonly IDictionary<string, NodeTeir> map = new Dictionary<string, NodeTeir> { 
            { "(S", NodeTeir.Clause },
            { "(SBAR", NodeTeir.Clause }, 
            { "(SBARQ", NodeTeir.Clause },
            {  "(SQ", NodeTeir.Clause },
            { "(SINV", NodeTeir.Clause },
            { "(VP", NodeTeir.Phrase },
            { "(NP", NodeTeir.Phrase },
            { "(PP", NodeTeir.Phrase },
            { "(ADVP", NodeTeir.Phrase },
            { "(ADJP", NodeTeir.Phrase },
            { "(PRT", NodeTeir.Phrase },
            { "(CONJP", NodeTeir.Phrase },
            { "(S", NodeTeir.Phrase },
            { "(SINV", NodeTeir.Phrase },
            { "(SQ", NodeTeir.Phrase },
            { "(SBARQ", NodeTeir.Phrase },
            { "(SBAR", NodeTeir.Phrase },
            { "(LST", NodeTeir.Phrase },
            { "(INTJ", NodeTeir.Phrase },
            { "(CC", NodeTeir.Word },
            { "(,", NodeTeir.Word },     
            { "(;", NodeTeir.Word },
            { "(:", NodeTeir.Word },
            { "(CD", NodeTeir.Word },
            { "(DT", NodeTeir.Word },
            { "(EX", NodeTeir.Word },
            { "(FW", NodeTeir.Word },
            { "(IN", NodeTeir.Word },
            { "(JJ", NodeTeir.Word },
            { "(JJR", NodeTeir.Word },
            { "(JJS", NodeTeir.Word },
            { "(LS", NodeTeir.Word },
            { "(-LRB-", NodeTeir.Word },
            { "(-RRB-", NodeTeir.Word },
            { "(''", NodeTeir.Word },
            { "(MD", NodeTeir.Word },
            { "(NN", NodeTeir.Word },
            { "(NNS", NodeTeir.Word },
            { "(NNP", NodeTeir.Word },
            { "(NNPS", NodeTeir.Word },
            { "(PDT", NodeTeir.Word },
            { "(POS", NodeTeir.Word },
            { "(PRP", NodeTeir.Word },
            { "(PRP$", NodeTeir.Word },
            { "(RB", NodeTeir.Word },
            { "(RBR", NodeTeir.Word },
            { "(RBS", NodeTeir.Word },
            { "(VB", NodeTeir.Word },
            { "(VBD", NodeTeir.Word },
            { "(VBG", NodeTeir.Word },
            { "(VBN", NodeTeir.Word },
            { "(VBP", NodeTeir.Word },
            { "(VBZ", NodeTeir.Word },
            { "(WDT", NodeTeir.Word },
            { "(WP", NodeTeir.Word },
            { "(WP$", NodeTeir.Word },
            { "(WRB", NodeTeir.Word },
            { "(RP", NodeTeir.Word },
            { "(SYM", NodeTeir.Word },
            { "(TO", NodeTeir.Word },
            { "(UH", NodeTeir.Word },
        };

        #endregion
    }
}
