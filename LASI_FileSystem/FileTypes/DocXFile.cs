using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a modern Word document (.docx).
    /// </summary>
    public sealed class DocXFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the DocXFile class for the given path.
        /// </summary>
        /// <param name="absolutePath">The path to a .docx file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .docx extension.</exception>
        public DocXFile(string absolutePath)
            : base(absolutePath) {
            if (!this.Ext.Equals(".docx", StringComparison.OrdinalIgnoreCase)) {
                throw new LASI.FileSystem.FileTypeWrapperMismatchException(GetType().ToString(), Ext);
            }
        }


    }
}
