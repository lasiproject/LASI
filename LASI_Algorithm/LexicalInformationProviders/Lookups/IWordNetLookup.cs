using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.Algorithm.LexicalInformationProviders.Lookups
{
    internal interface IWordNetLookup<TWord> where TWord : Word
    {
        /// <summary> 
        /// Loads relevant synyonym data and performs additional initialization.
        /// </summary>
        void Load();

        /// <summary>
        ///  Asynchronously loads relevant synyonym data and performs additional initialization.
        /// </summary>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        Task LoadAsync();
        /// <summary>
        /// Gets the synonyms for the given string treating it in the context of the Syntactic role of T.
        /// </summary>
        /// <param name="search">The string to lookup.</param>
        /// <returns>The synonyms for the given string treating it in the context of the Syntactic role of T.</returns>
        ISet<string> this[string search] {
            get;
        }
        /// <summary>
        /// Gets the synonyms for search.
        /// </summary>
        /// <param name="search">The TWord to lookup</param>
        /// <returns>The synonyms for search.</returns>
        ISet<string> this[TWord search] {
            get;
        }
    }
}
