using LASI;
using LASI.Core;
using LASI.Utilities;
using LASI.Content.TaggerEncapsulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace LASI.Content
{
    /// <summary>
    /// Parses pairs of text and tag tokens into their runtime type equivalents as specified by the Tagset
    /// </summary>
    internal class WordFactory
    {

        /// <summary>
        /// Initialized an instance of the TaggedWordParser class using the Tagset provided defined by the TaggingContext argument.
        /// </summary>
        /// <param name="taggingContext">The tagset-to-runtime-type mapping which will define how new word instances will be instantiated.</param>
        public WordFactory(WordTagsetMap taggingContext) { context = taggingContext; }

        /// <summary>
        /// Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="taggedText">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A new instance of the appropriate word type corresponding to the tag and containing the given text.</returns>
        public Word Create(TaggedText taggedText) {
            // TODO: Change this to not return null under any circumstances.
            if (string.IsNullOrWhiteSpace(taggedText.Text)) {
                throw new EmptyOrWhiteSpaceStringTaggedAsWordException(taggedText.Tag);
            }
            try {
                var createWord = context[taggedText.Tag];
                return createWord(taggedText.Text);
            } catch (EmptyWordTagException) {
                return new UnknownWord(taggedText.Text);
            } catch (UnknownWordTagException) {
                return taggedText.Tag.Length == 1 ? new Punctuator(taggedText.Text[0]) : new UnknownWord(taggedText.Text) as Word;
            }
        }

        private WordTagsetMap context;
    }
}
