using LASI;
using LASI.Algorithm;
using LASI.Utilities;
using LASI.ContentSystem.TaggerEncapsulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace LASI.ContentSystem
{
    /// <summary>
    /// Parses pairs of text and tag tokens into their runtime type equivalents as specified by the Tagset
    /// </summary>
    public class WordFactory
    {

        /// <summary>
        /// Initialized an instance of the TaggedWordParser class using the Tagset provided defined by the TaggingContext argument.
        /// </summary>
        /// <param name="taggingContext">The tagset-to-runtime-type mapping which will define how new word instances will be instantiated.</param>
        public WordFactory(WordTagsetMap taggingContext) { context = taggingContext; }

        /// <summary>
        /// Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="ttp">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A new instance of the appropriate word type corresponding to the tag and containing the given text.</returns>
        public Word Create(TextTagPair ttp) {
            if (string.IsNullOrWhiteSpace(ttp.Text)) { return null; }
            try {
                var wordCreator = context[ttp.Tag];
                return wordCreator(ttp.Text);
            }
            catch (EmptyWordTagException) {
                return new UnknownWord(ttp.Text);
            }
            catch (UnknownWordTagException) {
                return ttp.Tag.Length == 1 ? new Punctuator(ttp.Text[0]) : new UnknownWord(ttp.Text) as Word;
            }
        }

        private WordTagsetMap context;
    }
}
