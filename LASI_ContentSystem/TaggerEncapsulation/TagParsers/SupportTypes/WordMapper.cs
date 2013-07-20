using LASI.Algorithm;
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
    public class WordMapper
    {
        /// <summary>
        /// Initializes an instance of the TaggedWordParser class using the default Tagset (SharpNLP).
        /// <see cref="SharpNLPWordTagsetMap"/>
        /// </summary>
        public WordMapper() {
            context = new SharpNLPWordTagsetMap();
        }
        /// <summary>
        /// Initialized an instance of the TaggedWordParser class using the Tagset provided defined by the TaggingContext argument.
        /// </summary>
        /// <param name="taggingContext">The tagset-to-runtime-type mapping which will define how new word instances will be instantiated.</param>
        public WordMapper(WordTagsetMap taggingContext) {
            context = taggingContext;
        }

        /// <summary>
        /// Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="tag">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A new instance of the appropriate word type corresponding with the tag and containing the given text.</returns>
        public Word CreateWord(TextTagPair taggedText) {
            if (string.IsNullOrWhiteSpace(taggedText.Text))
                return null;
            return LookupMapping(taggedText.Tag)(taggedText.Text);
        }
        /// <summary>
        /// Returns a function which, when invoked, Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="taggedText">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A function which, when invoked, Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.</returns>
        public Func<Word> GetWordExpression(TextTagPair taggedText) {
            if (string.IsNullOrWhiteSpace(taggedText.Text))
                return null;
            var Constructor = LookupMapping(taggedText.Tag);
            return () => Constructor(taggedText.Text);
        }

        private Func<string, Word> LookupMapping(string tag) {
            tag = tag.Trim();
            try {
                var constructor = context[tag];
                return constructor;
            } catch (EmptyWordTagException) {
                return (s) => new LASI.Algorithm.UnknownWord(s);
            } catch (UnknownWordTagException) {
                if (tag.Length == 1) {
                    return (s) => (s == "." || s == "!" || s == "?") ? new SentenceEnding(s[0]) : new Punctuation(s[0]);
                } else {
                    //return (s) => new LASI.Algorithm.UndeterminedWord(s);
                    throw;
                }
            }
        }

        private WordTagsetMap context;
    }
}
