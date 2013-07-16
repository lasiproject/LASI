using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a raw text document (.txt).
    /// </summary>
    public sealed class TextFile : InputFile, LASI.Algorithm.IRawTextSource
    {
        /// <summary>
        /// Initializes a new instance of the TxtFile class for the given path.
        /// </summary>
        /// <param name="absolutePath">The path to a .txt file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .txt extension.</exception>
        public TextFile(string absolutePath)
            : base(absolutePath) {
            if (!this.Ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                throw new FileTypeWrapperMismatchException(GetType().ToString(), Ext);
        }


        /// <summary>
        /// Gets a single string containing all of the text in the TextFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TextFile.</returns>
        public string GetText() {
            using (var reader = new System.IO.StreamReader(this.FullPath)) {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// Asynchronously gets a single string containing all of the text in the TextFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TextFile.</returns>
        public async Task<string> GetTextAsync() {
            using (var reader = new System.IO.StreamReader(new System.IO.FileStream(this.FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))) { return await reader.ReadToEndAsync(); }
        }

        /// <summary>
        /// Gets the simple name of the TextFile.
        /// </summary>
        public string Name {
            get { return NameSansExt; }
        }
    }
}
