using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a raw text document (.txt).
    /// </summary>
    public sealed class TxtFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the TxtFile class for the given path.
        /// </summary>
        /// <param name="path">The path of the .txt file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .txt extension.</exception>
        public TxtFile(string path)
            : base(path) {
            if (!Ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                throw new FileTypeWrapperMismatchException(GetType().ToString(), Ext);
        }
        /// <summary>
        /// Gets a single string containing all of the text in the TextFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TextFile.</returns>
        public override string GetText() {
            using (var reader = File.OpenText(FullPath)) {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// Asynchronously gets a single string containing all of the text in the TextFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TextFile.</returns>
        public override async Task<string> GetTextAsync() {
            using (var reader = File.OpenText(FullPath)) {
                return await reader.ReadToEndAsync();
            }
        }
        /// <summary>
        /// The file extension, in lower case excluding a '.', of the file type an instance of the class wraps.
        /// </summary>
        public const string EXTENSION = "txt";
    }
}
