using LASI.Content.Exceptions;
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
        /// <exception cref="FileTypeWrapperMismatchException{TWrapper}">Thrown if the provided path does not end in the .txt extension.</exception>
        public TxtFile(string path)
            : base(path)
        {
            if (!Extension.Equals(CanonicalExtension, StringComparison.OrdinalIgnoreCase))
            {
                throw new FileTypeWrapperMismatchException<TxtFile>(Extension);
            }
        }

        /// <summary>
        /// Gets a single string containing all of the text in the TxtFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TxtFile.</returns>
        public override string LoadText()
        {
            using (var reader = File.OpenText(FullPath))
            {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// Asynchronously gets a single string containing all of the text in the TxtFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TxtFile.</returns>
        public override async Task<string> LoadTextAsync()
        {
            using (var reader = File.OpenText(FullPath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// The canonical file extension for the associated input file format.
        /// </summary>
        public override string CanonicalExtension { get; } = ".txt";
    }
}
