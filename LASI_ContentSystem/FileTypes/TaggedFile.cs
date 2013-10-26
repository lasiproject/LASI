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
    /// A strongly typed wrapper that encapsulates a tagged file (.tagged), a file with embedded syntactic annotations.
    /// </summary>
    public sealed class TaggedFile : InputFile, LASI.Core.ITaggedTextSource
    {
        /// <summary>
        /// Initializes a new instance of the TaggedFile class for the given path.
        /// </summary>
        /// <param name="filePath">The path to a .tagged file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .tagged extension.</exception>
        public TaggedFile(string filePath)
            : base(filePath) {
            if (!this.Ext.Equals(".tagged", StringComparison.OrdinalIgnoreCase))
                throw new LASI.ContentSystem.FileTypeWrapperMismatchException(GetType().ToString(), this.Ext);
        }
        /// <summary>
        /// Gets a single string containing all of the text in the TaggedFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TaggedFile.</returns>
        public override string GetText() {
            using (var reader = new StreamReader(this.FullPath)) {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// Asynchronously gets a single string containing all of the text in the TaggedFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TaggedFile.</returns>
        public override async Task<string> GetTextAsync() {
            using (var reader = new System.IO.StreamReader(
                new FileStream(
                    this.FullPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read))) { return await reader.ReadToEndAsync(); }
        }
    }
}
