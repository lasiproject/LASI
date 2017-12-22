using System.Threading.Tasks;
using LASI.Content.Exceptions;
using LASI.Content.FileConveters;

namespace LASI.Content.FileTypes
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a modern Word document (.docx).
    /// </summary>
    public sealed class DocXFile : InputFile<DocXFile>
    {
        /// <summary>
        /// Initializes a new instance of the DocXFile class for the given path.
        /// </summary>
        /// <param name="path">The path to a .docx file.</param>
        /// <exception cref="FileTypeWrapperMismatchException{DocXFile}">Thrown if the provided path does not end in the .docx extension.</exception>
        public DocXFile(string path) : base(path) { }


        /// <summary>
        /// Returns a single string containing all of the text in the DocXFile.
        /// </summary>
        /// <returns>A string containing all of the text in the DocXFile.</returns>
        public override string LoadText()
        {
            var converter = new DocxToTextConverter(this);
            return converter.ConvertFile().LoadText();
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the DocXFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the DocXFile.</returns>
        public override async Task<string> LoadTextAsync()
        {
            var converter = new DocxToTextConverter(this);
            var txtFile = await converter.ConvertFileAsync().ConfigureAwait(false);
            return await txtFile.LoadTextAsync().ConfigureAwait(false);
        }

        public override string CanonicalExtension => ".docx";
    }
}
