using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;
namespace LASI.FileSystem.TaggerEncapsulation
{
    public abstract class TagParser
    {
        public abstract LASI.Algorithm.DocumentConstructs.Document LoadDocument();
        public abstract System.Collections.Generic.IEnumerable<LASI.Algorithm.Paragraph> LoadParagraphs();


        public virtual async Task<IEnumerable<LASI.Algorithm.Paragraph>> LoadParagraphsAsync() {
            return await Task.Run(() => LoadParagraphs());
        }
        public virtual async Task<LASI.Algorithm.DocumentConstructs.Document> LoadDocumentAsync() {
            return await Task.Run(() => LoadDocument());
        }

        public LASI.FileSystem.FileTypes.TaggedFile TaggededDocumentFile {
            get;
            protected set;
        }
        /// <summary>
        /// Breaks a string of text containing multiple paragraphs into a collection of strings each representing an individual paragraph.
        /// Paragraphs are delimited using the default regular expression pattern "[\r\n]+[^]*[\r\n]+"
        /// </summary>
        /// <param name="line">a string containing the text to be broken down.</param>
        /// <returns>a collection of strings, each entry corresponding to the entire content of a single paragraph.</returns>
        protected virtual IEnumerable<string> ParseParagraphs(string data) {
            return from d in data.Split(new[] { "\r\n\r\n", "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries)
                   select d.Trim();
        }
        protected async virtual Task<IEnumerable<string>> ParseParagraphsAsync(string data) {
            return await Task.Run(() => ParseParagraphs(data));
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
