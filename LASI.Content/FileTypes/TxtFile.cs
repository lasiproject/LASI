using System.IO;
using System.Threading.Tasks;

namespace LASI.Content.FileTypes
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a raw text document (.txt).
    /// </summary>
    public sealed class TxtFile : InputFile<TxtFile>
    {
        public TxtFile(string path) : base(path) { }

        /// <summary>
        /// Gets a single string containing all of the text in the TxtFile.
        /// </summary>
        /// <returns>A single string containing all of the text in the TxtFile.</returns>
        public sealed override string LoadText()
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
        public sealed override async Task<string> LoadTextAsync()
        {
            using (var reader = File.OpenText(FullPath))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// The canonical file extension for the associated input file format.
        /// </summary>
        public sealed override string CanonicalExtension { get; } = ".txt";
    }
}
