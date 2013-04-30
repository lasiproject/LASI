using LASI.Utilities;
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
            /**
             * Noun Phrase Assumption:  The Last Noun in a Noun Phrase is the important one
             */
            Noun LastNoun = np.Words.OfType<Noun>().LastOrDefault();

            if (np.Words.Count() > 1 && LastNoun != null) {
                //////Output.WriteLine(np);
                /*
                foreach (Word verb in np.Words) {
                    ////Output.Write("[{0}] ", verb);
                }
                ////Output.WriteLine("\nLast Noun: {0}", LastNoun.Text);
                */


                /**
                 *  if word prior to LastNoun is also a Noun associate them
                 */
                if (LastNoun.PreviousWord is Noun) {
                    var PrevWrd = LastNoun.PreviousWord;
                    LastNoun.SuperTaxonomicNoun = (PrevWrd as Noun);
                    (PrevWrd as Noun).SubTaxonomicNoun = LastNoun;
                }


                /**
                 * Binding determiners to last noun
                 */
                Determiner det1 = np.Words.OfType<Determiner>().FirstOrDefault();
                if (det1 != null) {
                    LastNoun.BindDeterminer(det1);
                    det1.Determines = LastNoun;
               }


                /**
                 * Binding Adjectives to last noun
                 */
                var ListOfAdjectives = np.Words.GetAdjectives();
                if (ListOfAdjectives.Count() > 0) {
                    foreach (Adjective adj in ListOfAdjectives) {
                        LastNoun.BindDescriber(adj);
                        adj.Described = LastNoun;                       
                    }
                }


                /**
                 *  Binding first posessive pronoun to last noun
                 */
                var PosNoun = np.Words.OfType<PossessivePronoun>().FirstOrDefault();
                if (PosNoun != null) {
                    PosNoun.AddPossession(LastNoun);
                }              
            }
        }



        /**
         * Intra Verb Phrase Binding
         */
        public void IntraVerbPhrase(VerbPhrase vp) {
            Verb LastVerb = vp.Words.OfType<Verb>().LastOrDefault();

            if (vp.Words.Count() > 1 && LastVerb != null) {

                // Adverb linking to NEXT verb
                var adverbList = vp.Words.GetAdverbs();
                if (adverbList.Count() > 0) {
                    foreach (var advrb in adverbList) {
                        //////Output.WriteLine("adverb: {0}", advrb.Text);
                        var tempWrd = advrb.NextWord;
                        while (!(tempWrd is Verb)) {
                            tempWrd = tempWrd.NextWord;
                        }
                        var nextVerb = tempWrd as Verb;
                        nextVerb.ModifyWith(advrb);
                        //////Output.WriteLine("Next Verb: {0}", nextVerb.Text);
                    }
                }

                // "To" binding
                var toLinkerList = vp.Words.GetToLinkers();
                if (toLinkerList.Count() > 0) {
                    foreach (var toLink in toLinkerList) {
                        //////Output.WriteLine("To Linker: {0}", toLink.Text);
                        var prevWord = toLink.PreviousWord as Verb;
                        var nextWord = toLink.NextWord as Verb;

                        if (prevWord != null && nextWord != null) {
                            toLink.BindObjectOfPreposition(nextWord);
                            prevWord.AttachObjectViaPreposition(toLink);

                            if (nextWord != LastVerb) {
                                toLink.BindObjectOfPreposition(LastVerb);
                            }
                            //////Output.WriteLine("Prev: {0}, Next: {1}: , Last Verb: {2}", prevWord, nextWord, LastVerb);
                        } else {
                            toLink.BindObjectOfPreposition(LastVerb);
                        }
                    }
                }

                //  Binds all Modal Aux's to last verb
                var ModalAuxList = vp.Words.GetModalAuxilaries();
                if (ModalAuxList.Count() > 0)
                {
                    foreach (var ma in ModalAuxList)
                    {
                        LastVerb.Modality = ma;
                        ma.Modifies = LastVerb;
                    }
                }

                //Binds rhs verbs to last verb
                /*
                var VerbList = vp.Words.GetVerbs();
                if (VerbList.Count() > 0)
                {
                    foreach (var vrb in VerbList)
                    {
                        if (vrb != LastVerb)
                        {
                            ////Output.WriteLine("Verb: {0}", vrb.Text);
                        }
                    }
                }
                */


            }
        }
    }
}
