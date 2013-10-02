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
    public class WordMapper
    {
        /// <summary>
        /// Initializes an instance of the TaggedWordParser class using the default Tagset (SharpNLP).
        /// <see cref="SharpNLPWordTagsetMap"/>
        /// </summary>
        public WordMapper()
        {
            context = new SharpNLPWordTagsetMap();
        }
        /// <summary>
        /// Initialized an instance of the TaggedWordParser class using the Tagset provided defined by the TaggingContext argument.
        /// </summary>
        /// <param name="taggingContext">The tagset-to-runtime-type mapping which will define how new word instances will be instantiated.</param>
        public WordMapper(WordTagsetMap taggingContext)
        {
            context = taggingContext;
        }

        /// <summary>
        /// Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="taggedText">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A new instance of the appropriate word type corresponding to the tag and containing the given text.</returns>
        public Word CreateWord(TextTagPair taggedText)
        {
            if (string.IsNullOrWhiteSpace(taggedText.Text))
                return null;
            return LookupMapping(taggedText.Tag)(taggedText.Text);
            //catch (POSTagException) { return new UnknownWord(taggedText.Tag); }
        }
        /// <summary>
        /// Returns a function which, when invoked, Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="taggedText">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A function which, when invoked, Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.</returns>
        private Func<Word> GetWordExpression(TextTagPair taggedText)
        {
            if (taggedText.Text.IsNotWsOrNull()) {
                var Constructor = LookupMapping(taggedText.Tag);
                return () => Constructor(taggedText.Text);
            } else
                return null;

        }

        private Func<string, Word> LookupMapping(string tag)
        {
            tag = tag.Trim();
            try {
                var constructor = context[tag];
                return constructor;
            }
            catch (EmptyWordTagException) {
                return (t) => new LASI.Algorithm.UnknownWord(t);
            }
            catch (UnknownWordTagException) {
                if (tag.Length == 1) {
                    return (s) => (s == "." || s == "!" || s == "?") ? new SentenceEnding(s[0]) : new Punctuation(s[0]);
                } else {
                    return (t) => new LASI.Algorithm.UnknownWord(t);
                }
            }
        }
        /// <summary>
        /// Creates a new Lazy&lt;Word&gt; Instance which corresponds to the given text token and Part Of Speech tag. 
        /// </summary>
        /// <param name="taggedText">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A new Lazy&lt;Word&gt; instance of the appropriate Word type corresponding to the given tag and containing the given text.</returns>
        public Lazy<Word> GetLazyWord(TextTagPair taggedText)
        {
            var constructor = LookupMapping(taggedText.Tag);
            return new Lazy<Word>(() => constructor(taggedText.Text));
        }

        private WordTagsetMap context;
    }
}
