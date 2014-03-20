using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LASI.ContentSystem.TaggerEncapsulation
{
    abstract class TagParser
    {
        public abstract LASI.Core.DocumentStructures.Document LoadDocument();
        public abstract System.Collections.Generic.IEnumerable<Paragraph> LoadParagraphs();


        public abstract Task<LASI.Core.DocumentStructures.Document> LoadDocumentAsync();
        public LASI.ContentSystem.TaggedFile TaggededDocumentFile {
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
            return from d in data.SplitRemoveEmpty("\r\n\r\n", "\n\n", "<paragraph>", "</paragraph>")
                   select d.Trim();
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
