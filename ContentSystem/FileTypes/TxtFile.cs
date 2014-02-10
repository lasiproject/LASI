using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a raw text document (.txt).
    /// </summary>
    public sealed class TxtFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the TxtFile class for the given path.
        /// </summary>
        /// <param name="fullPath">The path to a .txt file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .txt extension.</exception>
        public TxtFile(string fullPath)
            : base(fullPath) {
            if (!this.Ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                throw new FileTypeWrapperMismatchException(GetType().ToString(), this.Ext);
        }


        /// <summary>
        /// Gets a single string containing all of the text in the TextFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TextFile.</returns>
        public override string GetText() {
            using (var reader = new System.IO.StreamReader(this.FullPath)) {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// Asynchronously gets a single string containing all of the text in the TextFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TextFile.</returns>
        public override async Task<string> GetTextAsync() {
            using (var reader = new System.IO.StreamReader(
                new System.IO.FileStream(this.FullPath,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read,
                    System.IO.FileShare.Read)
                    )
                ) {
                return await reader.ReadToEndAsync();
            }
        }
        /// <summary>
        /// The file extension, in lower case excluding a '.', of the file type an instance of the class wraps.
        /// </summary>
        public const string EXTENSION = "txt";
    }
}
