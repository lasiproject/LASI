using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LASI.Content.TaggerEncapsulation
{
    abstract class TagParser
    {
        public abstract Core.Document LoadDocument();
        public abstract IEnumerable<Core.Paragraph> LoadParagraphs();


        public abstract Task<LASI.Core.Document> LoadDocumentAsync();
        public LASI.Content.TaggedFile TaggedDocumentFile {
            get;
            protected set;
        }
        /// <summary>
        /// Breaks a string of text containing multiple paragraphs into a collection of strings each representing an individual parent.
        /// Paragraphs are delimited using the default regular expression pattern "[\r\n]+[^]*[\r\n]+"
        /// </summary>
        /// <param name="data">A string containing the text to be broken down.</param>
        /// <returns>A collection of strings, each entry corresponding to the entire content of a single Paragraph.</returns>
        protected virtual IEnumerable<string> ParseParagraphs(string data) {
            return data.SplitRemoveEmpty("\r\n\r\n", "\n\n", "<paragraph>", "</paragraph>").Select(datum => datum.Trim());
        }


        /// <summary>
        /// Gets the newPath of the tagged file which the TaggedFileParser governs.
        /// </summary>
        public string FilePath {
            get;
            protected set;
        }


        /// <summary>
        /// Gets the .tagged file object which encapsulates the input file which the TaggedFileParser governs.
        /// </summary>
        public string TaggedInputData {
            get;
            protected set;
        }
    }
}
