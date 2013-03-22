using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    public class InterPhraseWordBinding
    {
        public void InterNounPhrase(NounPhrase np) {
            //Accounts for there being more than one word in a entity
            if (np.Words.Count() > 1) {
                Noun LastNoun = np.Words.OfType<Noun>().Last();
                Determiner det1 = np.Words.OfType<Determiner>().FirstOrDefault();
                if (det1 != null)
                    LastNoun.BindDeterminer(det1);

                Console.WriteLine("Last Noun: {0}, Determined By: {1}", LastNoun, LastNoun.DeterminedBy);
            }
        }
    }
}
