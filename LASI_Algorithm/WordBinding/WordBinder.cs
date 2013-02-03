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
            NounHelper = new NounBindingHelper();
            VerbHelper = new VerbBindingHelper();
            TransitiveVerbHelper = new TransitiveVerbHelper();
            PronounHelper = new PronounBindingHelper();
            AdjectiveHelper = new AdjectiveBindingHelper();
            AdverbHelper = new AdverbBindingHelper();

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

        protected NounBindingHelper NounHelper {
            get;
            set;
        }

        protected PronounBindingHelper PronounHelper {
            get;
            set;
        }

        protected VerbBindingHelper VerbHelper {
            get;
            set;
        }

        public AdverbBindingHelper AdverbHelper {
            get;
            set;
        }

        public AdjectiveBindingHelper AdjectiveHelper {
            get;
            set;
        }

        public TransitiveVerbHelper TransitiveVerbHelper {
            get;
            set;
        }
    }
}
