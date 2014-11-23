using System;

namespace LASI.Core.Heuristics.WordNet
{
    internal abstract class WordNetLookup<TWord> : Progress<ResourceLoadEventArgs> where TWord : Word
    {
        /// <summary> 
        /// Loads relevant synonym data and performs additional initialization.
        /// </summary>
        internal abstract void Load();

        /// <summary>
        ///  Asynchronously loads relevant synonym data and performs additional initialization.
        /// </summary>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        internal virtual async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }
        /// <summary>
        /// Gets the synonyms for the given string treating it in the context of the Syntactic role of T.
        /// </summary>
        /// <param name="search">The string to lookup.</param>
        /// <returns>The synonyms for the given string treating it in the context of the Syntactic role of T.</returns>
        internal abstract System.Collections.Immutable.IImmutableSet<string> this[string search] {
            get;
        }
        /// <summary>
        /// Gets the synonyms for search.
        /// </summary>
        /// <param name="search">The TWord to lookup</param>
        /// <returns>The synonyms for search.</returns>
        internal abstract System.Collections.Immutable.IImmutableSet<string> this[TWord search] {
            get;
        }
        protected System.Collections.Generic.IEqualityComparer<string> IgnoreCase => StringComparer.OrdinalIgnoreCase;
        /// <summary>
        /// Constant value which specifies how the number of lines the header of a WordNet file.
        /// </summary>
        protected const int FILE_HEADER_LINE_COUNT = 29;
    }
}
