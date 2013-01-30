using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public class NounThesaurus : Thesaurus
    {
        /// <summary>
        /// Initializes a new instance of the NounThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym data for nouns.</param>
        public NounThesaurus(string filePath)
            : base(filePath) {
        }
        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parses the contents of the underlying WordNet database file Asynchronously in a new thread, returning Task object which represents the state of the ongoing operation.
        /// </summary>
        /// <returns>A Task Object which functions as a proxy the ongoing Asynchronous loading operation.</returns>
        public override System.Threading.Tasks.Task LoadAsync() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets a RelatedWordSet containing all identified synonyms for the given Noun.
        /// </summary>
        /// <param name="toMatch">The Word instance to get synonyms for.</param>
        /// <returns>The collection of synonyms as strings.</returns>
        public override SynonymSet GetMatches(Word toMatch) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets a RelatedWordSet containing all identified synonyms for the given Noun.
        /// </summary>
        /// <param name="toMatch">The raw text of a word to get synonyms for.</param>
        /// <returns>The collection of synonyms as strings.</returns>
        public override SynonymSet GetMatches(string toMatch) {
            throw new NotImplementedException();
        }

    }
}
