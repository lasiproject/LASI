using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.ContentSystem
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates an Acrobat document (.pdf).
    /// </summary>
    public sealed class PdfFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the PdfFile class for the given path.
        /// </summary>
        /// <param name="absolutePath">The path to a .pdf file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .pdf extension.</exception>
        public PdfFile(string absolutePath)
            : base(absolutePath) {
            if (!this.Ext.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                throw new LASI.ContentSystem.FileTypeWrapperMismatchException(GetType().ToString(), this.Ext);

        }
        /// <summary>
        /// Returns a single string containing all of the text in the PdfFile.
        /// </summary>
        /// <returns>A string containing all of the text in the PdfFile.</returns>
        public override string GetText() {
            var converter = new PdfToTextConverter(this);
            return (converter.ConvertFile() as TextFile).GetText();
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the PdfFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the PdfFile.</returns>
        public override async Task<string> GetTextAsync() {
            var converter = new PdfToTextConverter(this);
            return await (await converter.ConvertFileAsync() as TextFile).GetTextAsync();
        }

    }
}
