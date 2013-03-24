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

                /**
                 * Noun Phrase Assumption:  The Last Noun in a Noun Phrase is the important one
                 */
                Noun LastNoun = np.Words.OfType<Noun>().Last();
                

                
                foreach (Word w in np.Words)
                {
                    Console.Write("[{0}] ", w);
                }
                Console.Write("\n------\n");
                



                /**
                 * Binding determiners to last noun
                 */
                Determiner det1 = np.Words.OfType<Determiner>().FirstOrDefault();
                if (det1 != null)
                {
                    LastNoun.BindDeterminer(det1);
                    det1.Determines = LastNoun;
                    //Console.WriteLine("Last Noun: {0}, Determined By: {1}", LastNoun, LastNoun.DeterminedBy);
                    //Console.WriteLine("Determiner: {0}, Determines: {1}", det1, det1.Determines);
                }

            
                /**
                 * Binding Adjectives to last noun
                 */
                var ListOfAdjectives = np.Words.GetAdjectives();
                if (ListOfAdjectives.Count() > 0)
                {
                    foreach (Adjective adj in ListOfAdjectives)
                    {
                        LastNoun.BindDescriber(adj);
                        adj.Described = LastNoun;
                    }

                    /*
                    Console.Write("Last Noun: {0} => Described By: ", LastNoun.Text);
                    foreach (Adjective adj in LastNoun.DescribedBy)
                    {
                        Console.Write("{0}, ", adj.Text);
                    }
                    Console.WriteLine('\n');
                    */ 
                    /*
                    foreach (Adjective adj in ListOfAdjectives)
                    {
                        Console.WriteLine("Adjective: {0}, Describes: {1}", adj.Text, adj.Described.Text);
                    }
                    */ 


                }

                /**
                 *  Binding first posessive pronoun to last noun
                 */
                 
                if(np.Words.GetPronouns().FirstOrDefault() != null)
                {
                    Pronoun PosNoun = np.Words.GetPronouns().FirstOrDefault();
                    Console.Write("Pronoun: {0}", PosNoun);
                }




            }
        }
    }
}
