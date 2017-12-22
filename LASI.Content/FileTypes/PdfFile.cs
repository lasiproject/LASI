using System.Threading.Tasks;
using LASI.Content.Exceptions;
using LASI.Content.FileConveters;

namespace LASI.Content.FileTypes
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates an Acrobat document (.pdf).
    /// </summary>
    public sealed class PdfFile : InputFile<PdfFile>
    {
        /// <summary>
        /// Initializes a new instance of the PdfFile class for the given path.
        /// </summary>
        /// <param name="fullPath">The path to a .pdf file.</param>
        /// <exception cref="FileTypeWrapperMismatchException{PdfFile}">Thrown if the provided path does not end in the .pdf extension.</exception>
        public PdfFile(string fullPath) : base(fullPath) { }

        /// <summary>
        /// Returns a single string containing all of the text in the PdfFile.
        /// </summary>
        /// <returns>A string containing all of the text in the PdfFile.</returns>
        public override string LoadText() => new PdfToTextConverter(this).ConvertFile().LoadText();

        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the PdfFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the PdfFile.</returns>
        public override async Task<string> LoadTextAsync()
        {
            var converter = new PdfToTextConverter(this);
            var converted = await converter.ConvertFileAsync().ConfigureAwait(false);
            return await converted.LoadTextAsync().ConfigureAwait(false);
        }

        public override string CanonicalExtension => ".pdf";
    }
}
