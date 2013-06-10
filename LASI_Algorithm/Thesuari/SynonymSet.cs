using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    /// <summary>
    /// Represents a Synonym set entry corresponding to a line in a WordNet thesaurus file.
    /// This type is used within the various Thesaurus implementations to compose and query the contents of the WordNet database files.
    /// This class is internal forbidding instantiation outside of the Thesaurus Namespace.
    /// </summary>
    internal class VerbThesaurusSynSet : IReadOnlyCollection<string>
    {
        public VerbThesaurusSynSet(IEnumerable<int> referencedSetIds, IEnumerable<string> memberWords, WordNetVerbCategory lexName)
        {
            Words = new HashSet<string>(memberWords);
            ReferencedIndexes = new HashSet<int>(referencedSetIds);
            LexName = lexName;
            Index = referencedSetIds.First();
        }


        /// <summary>
        /// Gets the members directly contained within the VerbThesaurusSynSet.
        /// </summary>
        public IEnumerable<string> Words
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the collection of VerbThesaurusSynSet-index-codes corresponding to referenced SynonymSets.
        /// </summary>
        public IEnumerable<int> ReferencedIndexes
        {
            get;
            private set;

        }
        /// <summary>
        /// Gets the Index which identifies the VerbThesaurusSynSet.
        /// </summary>
        public int Index
        {
            get;
            private set;
        }
        /// <summary>
        /// Returns a single string representing the members of the VerbThesaurusSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the VerbThesaurusSynSet.</returns>
        public override string ToString()
        {
            return "[" + Index + "] " + Words.Aggregate("", (str, code) =>
            {
                return str + "  " + code;
            });
        }

        /// <summary>
        /// Gets the number of direct members contained in the VerbThesaurusSynSet.
        /// </summary>
        public int Count
        {
            get
            {
                return Words.Count();
            }
        }

        /// <summary>
        /// Exposes an which enumerator exposes the direct wd members when the VerbThesaurusSynSet is enumerated.
        /// </summary>
        /// <returns>An enumerator which exposes the direct wd members when the VerbThesaurusSynSet is enumerated.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return Words.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public WordNetVerbCategory LexName
        {
            get;
            set;
        }
    }
}
