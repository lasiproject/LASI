using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Thesauri
{
    public class NounThesaurus : Thesaurus
    {
        /// <summary>
        /// Initializes a new instance of the NounThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for nouns.</param>
        public NounThesaurus(string filePath)
            : base(filePath) {
        }
        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            throw new NotImplementedException();
        }


        public override IEnumerable<string> this[string search] {
            get {
                throw new NotImplementedException();
            }
        }

        public override IEnumerable<string> this[Word search] {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
