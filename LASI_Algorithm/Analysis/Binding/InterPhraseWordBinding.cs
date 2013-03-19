using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class InterPhraseWordBinding
    {
        public void InterNounPhrase(NounPhrase np)
        {
            //Accounts for there being more than one word in a phrase
            if (np.Words.Count() > 1)
            {
                Console.WriteLine(np);
                /*
                 * Go through words.
                 * and set specific properties
                 */
               /* var nouns = np.Words.GetNouns();

               
                var determiner = np.Words.FirstOrDefault(w => w is Determiner) as Determiner;
                foreach (var n in nouns)
                {
                    determiner.Determines = n;
                }*/
            }

            foreach (var noun in np.Words.GetNouns()) {
                
            }
        }
    }
}
        