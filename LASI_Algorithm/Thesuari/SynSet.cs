using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesuari
{
    public class SynSet{

        public string setID;
        public List<string> setWords;
        public List<string> setPointers;

        public SynSet(string ID, List<string> words, List<string> pointers)
        {

            setID = ID;
            setWords = words;
            setPointers = pointers;

        }

        public string ID
        {
            get { return setID; }

        }

       
        public List<string> Words
        {
            get { return setWords; }

        }

        public List<string> Pointers
        {
            get { return setPointers; }
        }

    }
}
