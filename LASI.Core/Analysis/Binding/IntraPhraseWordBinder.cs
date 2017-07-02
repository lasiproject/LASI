using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Binding
{
    /// <summary>
    /// Provides various methods facilitate bindings between the words within a single phrase.
    /// </summary>
    public static class IntraPhraseWordBinder
    {
        /// <summary>
        /// Binds some of the words within a NounPhrase.
        /// </summary>
        /// <param name="np">The NounPhrase to bind within.</param>
        public static void Bind<TNounPhrase>(TNounPhrase np) where TNounPhrase : NounPhrase
        {
            /*
             * Noun Phrase Assumption:  The Last Noun in a Noun Phrase is the important one
             */
            var LastNoun = np.Words.OfType<Noun>().LastOrDefault();

            if (np.Words.Count() > 1 && LastNoun != null)
            {
                //////Output.WriteLine(nps);
                /*
                foreach (word adverb in nps.Words) {
                    ////Output.Write("[{0}] ", adverb);
                }
                ////Output.WriteLine("\nLast Noun: {0}", LastNoun.Text);
                */


                /*  
                 *  if word prior to LastNoun is also a Noun associate them
                 */

                if (LastNoun.PreviousWord is Noun previousAsNoun)
                {
                    LastNoun.PrecedingAdjunctNoun = previousAsNoun;
                    previousAsNoun.FollowingAdjunctNoun = LastNoun;
                }


                /*
                 * Binding determiners to last noun
                 */
                var det1 = np.Words.OfType<Determiner>().FirstOrDefault();
                if (det1 != null)
                {
                    LastNoun.BindDeterminer(det1);
                }


                /*
                 * Binding Adjectives to last noun
                 */
                var ListOfAdjectives = np.Words.OfAdjective();
                if (ListOfAdjectives.Any())
                {
                    foreach (var adj in ListOfAdjectives)
                    {
                        LastNoun.BindDescriptor(adj);
                    }
                }


                /*
                 *  Binding first possessive pronoun to last noun
                 */
                var PosNoun = np.Words.OfType<PossessivePronoun>().FirstOrDefault();
                if (PosNoun != null)
                {
                    PosNoun.AddPossession(LastNoun);
                }
            }
        }



        /// <summary>
        /// Intra Verb Phrase Binding
        /// </summary>
        /// <param name="vp">The VerbPhrase whose elements will be bound together.</param>
        public static void Bind(VerbPhrase vp)
        {
            var LastVerb = vp.Words.OfType<Verb>().LastOrDefault();

            if (vp.Words.Count() > 1 && LastVerb != null)
            {
                // Adverb linking to NEXT adverb
                var adverbList = vp.Words.OfAdverb();
                if (adverbList.Any())
                {
                    foreach (var advrb in adverbList)
                    {
                        //////Output.WriteLine("adverb: {0}", advrb.Text);
                        var tempWrd = advrb.NextWord;
                        while (!(tempWrd is Verb))
                        {
                            tempWrd = tempWrd.NextWord;
                        }
                        var nextVerb = tempWrd as Verb;
                        nextVerb.ModifyWith(advrb);
                        //////Output.WriteLine("Next Verb: {0}", nextVerb.Text);
                    }
                }

                // "To" binding
                var toLinkerList = vp.Words.OfToLinker();
                if (toLinkerList.Any())
                {
                    foreach (var toLink in toLinkerList)
                    {
                        //////Output.WriteLine("To Linker: {0}", toLink.Text);
                        var prevWord = toLink.PreviousWord as Verb;
                        var nextWord = toLink.NextWord as Verb;

                        if (prevWord != null && nextWord != null)
                        {
                            toLink.BindObject(nextWord);
                            prevWord.AttachObjectViaPreposition(toLink);

                            if (nextWord != LastVerb)
                            {
                                toLink.BindObject(LastVerb);
                            }
                            //////Output.WriteLine("Prev: {0}, Next: {1}: , Last Verb: {2}", prevWord, nextWord, LastVerb);
                        }
                        else {
                            toLink.BindObject(LastVerb);
                        }
                    }
                }

                //  Binds all Modal Aux's to last adverb
                var ModalAuxList = vp.Words.OfModal();
                if (ModalAuxList.Any())
                {
                    foreach (var ma in ModalAuxList)
                    {
                        LastVerb.Modality = ma;
                        ma.Modifies = LastVerb;
                    }
                }

                //Binds second verbs to last adverb
                /*
                var VerbList = verbPhrase.Words.GetVerbs();
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
