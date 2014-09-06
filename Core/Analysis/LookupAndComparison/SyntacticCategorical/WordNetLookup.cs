using LASI;
using LASI.Core;
using LASI.Core.Interop.Reporting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace LASI.Core.Heuristics.WordNet
{
    internal abstract class WordNetLookup<TWord, TSetLink> : Progress<ResourceLoadEventArgs> where TWord : Word
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
        internal virtual IEnumerable<KeyValuePair<TSetLink, int>> ParseReferencedSets(string line, Func<IList<string>, KeyValuePair<TSetLink, int>> setReferenceFactory) {
            return from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                   let split = match.Value.SplitRemoveEmpty(' ')
                   where split.Count() > 1 && InterSetMap.ContainsKey(split[0])
                   select setReferenceFactory(split);
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

        protected abstract IReadOnlyDictionary<string, TSetLink> InterSetMap {
            get;
        }
        protected const string pointerRegex = @"\D{1,2}\s*\d{8}";
        protected const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";
        protected const int HEADER_LENGTH = 29;

    }
}
