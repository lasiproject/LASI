using LASI.Content.Exceptions;
using LASI.Core;
using LASI.Content.Tagging;

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
        public WordFactory(WordTagsetMap taggingContext) { wordTagset = taggingContext; }

        /// <summary>
        /// Creates a new Instance of the Word class which corresponds to the given text token and Part Of Speech tag.
        /// </summary>
        /// <param name="taggedText">A Word or Punctuation string and its associated Part Of Speech tag.</param>
        /// <returns>A new instance of the appropriate word type corresponding to the tag and containing the given text.</returns>
        /// <exception cref="UnknownWordTagException"/>
        /// <exception cref="EmptyWordTagException"/>
        /// <exception cref="EmptyOrWhiteSpaceStringTaggedAsWordException"/>
        public Word Create(TaggedText taggedText)
        {
            if (string.IsNullOrWhiteSpace(taggedText.Text))
            {
                throw new EmptyOrWhiteSpaceStringTaggedAsWordException(taggedText.Tag);
            }

            try
            {
                return wordTagset[taggedText.Tag](taggedText.Text);
            }
            catch (EmptyWordTagException)
            {
                throw;
            }
            catch (UnknownWordTagException)
            {
                if (taggedText.Tag.Length == 1) // this is dubious
                {
                    return new Punctuator(taggedText.Text[0]);
                }
                throw;
            }
        }

        private readonly WordTagsetMap wordTagset;
    }
}
