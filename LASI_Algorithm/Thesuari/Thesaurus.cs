using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public abstract class Thesaurus
    {
        /// <summary>
        /// Constructor accessible only to derrived classes.
        /// Provides common initialization logic.
        /// </summary>
        /// <param name="filePath">The path of WordNet database file which provides the synonym line (form should be line.pos, e.g. line.verb)</param>
        protected Thesaurus(string filePath) {
            FilePath = filePath;
        }
        /// <summary>
        /// gets or sets the path of the WordNet database file which this thesaurus is built on
        /// </summary>
        protected virtual string FilePath {
            get;
            set;
        }
        /// <summary>
        /// When overriden in a derrived class, this method
        /// Loads the database file and performs additional initialization
        /// </summary>
        public abstract void Load();

        public virtual async Task LoadAsync() {
            LoadingStatus = ThesaurusLoadingState.Initiated;
            await Task.Run(() => Load());
            LoadingStatus = ThesaurusLoadingState.Completed;
        }

        public abstract IEnumerable<string> this[string search] {
            get;
        }

        public abstract IEnumerable<string> this[Word search] {
            get;
        }

        /// <summary>
        ///gets the current state of the file loading process
        /// </summary>
        public virtual ThesaurusLoadingState LoadingStatus {
            get;
            protected set;
        }

        /// <summary>
        /// gets or sets all of the synsets in the Thesaurus
        /// </summary>
        internal virtual IDictionary<string, SynonymSet> AssociationData {
            get;
            set;
        }
    }
}
