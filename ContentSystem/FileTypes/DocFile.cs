using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.ContentSystem
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a legacy Word document (.doc).
    /// </summary>
    public sealed class DocFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the DocFile class for the given path.
        /// </summary>
        /// <param name="absolutePath">The path to a .doc file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .doc extension.</exception>
        public DocFile(string absolutePath)
            : base(absolutePath) {
            if (!this.Ext.Equals(".doc", StringComparison.OrdinalIgnoreCase)) {
                throw new LASI.ContentSystem.FileTypeWrapperMismatchException(GetType().ToString(), this.Ext);
            }
        }

        /// <summary>
        /// Returns a single string containing all of the text in the DocFile.
        /// </summary>
        /// <returns>A string containing all of the text in the DocFile.</returns>
        public override string GetText() {
            DocXFile docx;
            var todocXConverter = new DocToDocXConverter(this);
            try {
                docx = todocXConverter.ConvertFile() as DocXFile;
            }
            catch (Exception e) { throw new FileConversionFailureException(FullPath, "DOC", "DOCX", e); }
            return docx.GetText();
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the DocFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the DocFile.</returns>
        public override async Task<string> GetTextAsync() {
            DocXFile docx;
            var toDocXConverter = new DocToDocXConverter(this);
            try {
                docx = await toDocXConverter.ConvertFileAsync() as DocXFile;
            }
            catch (Exception e) { throw new FileConversionFailureException(FullPath, "DOC", "DOCX", e); }
            return await docx.GetTextAsync();
        }
    }

}
