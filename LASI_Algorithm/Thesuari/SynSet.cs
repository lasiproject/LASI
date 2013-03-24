using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public class SynSet
    {

        public string setID;
        public List<string> setWords;
        public List<string> setPointers;


        //Aluan: I added this field to store some additional information I found in the WordNet files

        private WordNetNounLex lexName;


        //Aluan: I added this Property to access some additional information I found in the WordNet files

        public WordNetNounLex LexName {
            get {
                return lexName;
            }
        }

        //Aluan: I added this constructor to include some additional information I found in the WordNet files

        public SynSet(string ID, List<string> words, List<string> pointers, WordNetNounLex lexCategory) {

            setID = ID;
            setWords = words;
            setPointers = pointers;
            lexName = lexCategory;

        }

        public SynSet(string ID, List<string> words, List<string> pointers) {

            setID = ID;
            setWords = words;
            setPointers = pointers;

        }

        public string ID {
            get {
                return setID;
            }

        }


        public List<string> Words {
            get {
                return setWords;
            }

        }

        public List<string> Pointers {
            get {
                return setPointers;
            }
        }



    }
}
