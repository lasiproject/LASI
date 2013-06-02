using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public class SynSet
    {

        private string setID;


        private HashSet<string> setWords;
        private HashSet<string> setPointers;


        //Aluan: I added this field to store some additional information I found in the WordNet files

        private WordNetNounCategory lexName;


        //Aluan: I added this Property to access some additional information I found in the WordNet files

        public WordNetNounCategory LexName
        {
            get
            {
                return lexName;
            }
        }

        //Aluan: I added this constructor to include some additional information I found in the WordNet files

        public SynSet(string ID, IEnumerable<string> words, IEnumerable<string> pointers, WordNetNounCategory lexCategory)
        {

            setID = ID;
            setWords = new HashSet<string>(words);
            setPointers = new HashSet<string>(pointers);
            lexName = lexCategory;

        }

        public SynSet(string ID, IEnumerable<string> words, IEnumerable<string> pointers)
        {

            setID = ID;
            setWords = new HashSet<string>(words);
            setPointers = new HashSet<string>(pointers);

        }

        public string ID
        {
            get
            {
                return setID;
            }

        }


        public HashSet<string> SetWords
        {
            get
            {
                return setWords;
            }

        }

        public HashSet<string> SetPointers
        {
            get
            {
                return setPointers;
            }
        }

        public string SetID
        {
            get
            {
                return setID;
            }
            set
            {
                setID = value;
            }
        }

    }
}
