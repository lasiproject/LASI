using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    public class InterPhraseWordBinding
    {
        public void IntraNounPhrase(NounPhrase np) {
            //Accounts for there being more than one word in a entity

            /**
             * Noun Phrase Assumption:  The Last Noun in a Noun Phrase is the important one
             */
            Noun LastNoun = np.Words.OfType<Noun>().LastOrDefault();


            if (np.Words.Count() > 1 && LastNoun != null) {
                

                
                foreach (Word w in np.Words)
                {
                    Console.Write("[{0}] ", w);
                }
                Console.Write("\nLast Noun: {0}", LastNoun);
                Console.Write("\n------\n");
                


                /**
                 * Binding determiners to last noun
                 */
                Determiner det1 = np.Words.OfType<Determiner>().FirstOrDefault();
                if (det1 != null)
                {
                    LastNoun.BindDeterminer(det1);
                    det1.Determines = LastNoun;
                    //Console.WriteLine("Last Noun: {0}, Determined By: {1}", LastNoun.Text, LastNoun.DeterminedBy.Text);
                    //Console.WriteLine("Determiner: {0}, Determines: {1}", det1.Text, det1.Determines.Text);
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
                var PosNoun = np.Words.OfType<PossessivePronoun>().FirstOrDefault();
                if (PosNoun != null)
                {
                    PosNoun.AddPossession(LastNoun);
                    
                    
                    /*
                    Console.Write("Pronoun: {0} => ", PosNoun.Text);
                    foreach (var p in PosNoun.Possessed)
                    {
                        Console.Write("  {0}", p.Text);
                    }
                    Console.Write("\n");
                    */
                }




            }
        }

        public void IntraVerbPhrase(VerbPhrase vp)
        {
            Verb LastVerb = vp.Words.OfType<Verb>().LastOrDefault();
            if (vp.Words.Count() > 0 && LastVerb != null)
            {
                foreach (Word w in vp.Words)
                {
                    Console.Write("{0}, ", w);
                }
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~\n");
            }
        }
    }
}
