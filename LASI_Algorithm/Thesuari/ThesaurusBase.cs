using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public abstract class ThesaurusBase
    {
        /// <summary>
        /// Constructor accessible only to derrived classes.
        /// Provides common initialization logic.
        /// </summary>
        /// <param name="filePath">The path of WordNet database file which provides the synonym line (form should be line.pos, e.g. line.verb)</param>
        protected ThesaurusBase(string filePath) {
            FilePath = filePath;
        }
        /// <summary>
        /// gets or sets the path of the WordNet database file which this thesaurus is built on
        /// </summary>
        protected string FilePath {
            get;
            set;
        }
        /// <summary>
        /// When overriden in a derrived class, this method
        /// Loads the database file and performs additional initialization
        /// </summary>
        public abstract void Load();

        public virtual async Task LoadAsync() {
            await Task.Run(() => Load());
        }

        public abstract HashSet<string> this[string search] {
            get;
        }

        public abstract HashSet<string> this[Word search] {
            get;
        }
        /// <summary>
        /// gets or sets all of the synsets in the ThesaurusBase
        /// </summary>
        internal IDictionary<string, SynonymSet> AssociationData {
            get;
            set;
        }
    }
}
