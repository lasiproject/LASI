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
            if (np.Words.Count() > 1)
            {
                Console.WriteLine(np);
            }
        }
    }
}
