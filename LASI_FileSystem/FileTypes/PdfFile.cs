using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
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
                throw new LASI.FileSystem.FileTypeWrapperMismatchException(GetType().ToString(), Ext);

        }
    }
}
