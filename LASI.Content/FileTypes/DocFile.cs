using System.IO;
using System.Threading.Tasks;
using LASI.Content.FileConverters;

namespace LASI.Content.FileTypes
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a legacy Word document (.doc).
    /// </summary>
    public sealed class DocFile : InputFile<DocFile>
    {
        /// <summary>
        /// Initializes a new instance of the DocFile class for the given path.
        /// </summary>
        /// <param name="path">The path to a .doc file.</param>
        /// <exception cref="Exceptions.FileTypeWrapperMismatchException{TWrapper}">Thrown if the provided path does not end in the .doc extension.</exception>
        public DocFile(string path) : base(path) { }

        /// <summary>
        /// Returns a single string containing all of the text in the DocFile.
        /// </summary>
        /// <returns>A string containing all of the text in the DocFile.</returns>
        public override string LoadText()
        {
            var todocXConverter = new DocToDocXConverter(this);
            try
            {
                return todocXConverter.ConvertFile().LoadText();
            }
            catch (IOException e)
            {
                throw CreateFileConversionFailureException("DOCX", e);
            }
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the DocFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the DocFile.</returns>
        public override async Task<string> LoadTextAsync()
        {
            var toDocXConverter = new DocToDocXConverter(this);
            try
            {
                var docx = await toDocXConverter.ConvertFileAsync().ConfigureAwait(false);
                return await docx.LoadTextAsync().ConfigureAwait(false);
            }
            catch (IOException e)
            {
                throw CreateFileConversionFailureException("DOCX", e);
            }
        }
        /// <summary><c>".doc"</c></summary>
        public override string CanonicalExtension => ".doc";
    }
}
