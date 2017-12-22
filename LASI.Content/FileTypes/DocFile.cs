using System.Threading.Tasks;
using LASI.Content.FileConveters;
using LASI.Utilities;
using LASI.Utilities.Validation;
using FileTypeWrapperMismatchException = LASI.Content.Exceptions.FileTypeWrapperMismatchException<LASI.Content.FileTypes.DocFile>;

namespace LASI.Content.FileTypes
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a legacy Word document (.doc).
    /// </summary>
    public sealed class DocFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the DocFile class for the given path.
        /// </summary>
        /// <param name="path">The path to a .doc file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .doc extension.</exception>
        public DocFile(string path) : base(path)
        {
            Validate.Equals(Extension, CanonicalExtension);
            if (!Extension.EqualsIgnoreCase(CanonicalExtension))
            {
                throw new FileTypeWrapperMismatchException(Extension);
            }
        }


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
            catch (System.IO.IOException e)
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
            catch (System.IO.IOException e)
            {
                throw CreateFileConversionFailureException("DOCX", e);
            }
        }

        public override string CanonicalExtension => ".doc";
    }
}
