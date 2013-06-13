using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public class SynSet
    {



        //Aluan: I added this field to store some additional information I found in the WordNet files



        //Aluan: I added this Property to access some additional information I found in the WordNet files

        public WordNetNounCategory LexName {
            get;
            private set;
        }

        //Aluan: I added this constructor to include some additional information I found in the WordNet files

        public SynSet(int ID, IEnumerable<string> words, IEnumerable<int> pointers, WordNetNounCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            Pointers = new HashSet<int>(pointers);
            LexName = lexCategory;

        }

        public SynSet(int ID, IEnumerable<string> words, IEnumerable<int> pointers) {

            this.ID = ID;
            Words = new HashSet<string>(words);
            Pointers = new HashSet<int>(pointers);
            LexName = WordNetNounCategory.ARBITRARY;
        }




        public HashSet<string> Words {
            get;
            private set;

        }

        public HashSet<int> Pointers {
            get;
            private set;
        }

        public int ID {
            get;
            private set;
        }

    }
}
