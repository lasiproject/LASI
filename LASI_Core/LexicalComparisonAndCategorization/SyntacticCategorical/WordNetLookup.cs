using LASI;
using LASI.Core;
using LASI.Core.Interop.Reporting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics
{
    internal abstract class WordNetLookup<TWord> : Progress<ResourceLoadedEventArgs> where TWord : Word
    {
        /// <summary> 
        /// Loads relevant synyonym data and performs additional initialization.
        /// </summary>
        internal abstract void Load();

        /// <summary>
        ///  Asynchronously loads relevant synyonym data and performs additional initialization.
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
        internal abstract ISet<string> this[string search] {
            get;
        }
        /// <summary>
        /// Gets the synonyms for search.
        /// </summary>
        /// <param name="search">The TWord to lookup</param>
        /// <returns>The synonyms for search.</returns>
        internal abstract ISet<string> this[TWord search] {
            get;
        }
        protected const int HEADER_LENGTH = 29;

    }
}
