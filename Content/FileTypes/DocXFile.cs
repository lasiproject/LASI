using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Content
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a modern Word document (.docx).
    /// </summary>
    public sealed class DocXFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the DocXFile class for the given path.
        /// </summary>
        /// <param name="path">The path to a .docx file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .docx extension.</exception>
        public DocXFile(string path)
            : base(path) {
            if (!Extension.Equals(".docx", StringComparison.OrdinalIgnoreCase)) {
                throw new FileTypeWrapperMismatchException(GetType().ToString(), Extension);
            }
        }
        /// <summary>
        /// Returns a single string containing all of the text in the DocXFile.
        /// </summary>
        /// <returns>A string containing all of the text in the DocXFile.</returns>
        public override string LoadText() {
            var converter = new DocxToTextConverter(this);
            return (converter.ConvertFile() as TxtFile).LoadText();
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the DocXFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the DocXFile.</returns>
        public override async Task<string> LoadTextAsync() {
            var converter = new DocxToTextConverter(this);
            return await (await converter.ConvertFileAsync() as TxtFile).LoadTextAsync();
        } 
    }
}
