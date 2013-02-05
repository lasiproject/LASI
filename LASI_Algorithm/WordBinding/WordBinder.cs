using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.WordBinding
{
    /// <summary>
    /// Binds a collection of words together in linear order
    /// </summary>
    class WordBinder
    {
        public WordBinder() {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(Word anyWord) {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(Noun noun) {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(Verb verb) {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(TransitiveVerb transitiveVerb) {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(Pronoun pronoun) {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(Adjective adjective) {
            throw new NotImplementedException();
        }
        public void ProcessNextWord(Adverb adverb) {
            throw new NotImplementedException();
        }
        public List<Word> BindAll(List<Word> words) {
            throw new NotImplementedException();
            foreach (var word in words) {
                ProcessNextWord(word as dynamic);
            }
            return words;
        }
    }
}
