using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm;
using LASI.Utilities;
using LASI.Algorithm.DocumentConstructs;


namespace LASI.Algorithm.Binding
{
    public class PronounBinder
    {
        public void Bind(Document document)
        {
            BindPosessivePronouns(document);
        }
        /// <summary>
        /// Bind posessive pronouns located in the objects of a sentence to the proper noun in the subject of that sentence. 
        /// Example Sentence that this applies to:
        /// "LASI binds it's pronouns."
        /// Pronoun "it's" binds to the proper noun "LASI"
        /// </summary>
        /// <param name="doc">Document for analysis</param>
        private void BindPosessivePronouns(Document doc)
        { //Aluan Says: Beautiful function man.
            foreach (var s in doc.Sentences) {
                foreach (VerbPhrase vp in from VerbPhrase p in s.Phrases.GetVerbPhrases().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                          where p.DirectObjects.Any() ||
                                                p.IndirectObjects.Any()
                                          where p.DirectObjects.Any(direct => direct is PossessivePronoun) ||
                                                p.IndirectObjects.Any(indirect => indirect is PossessivePronoun)
                                          where p.Subjects.Any(subject => subject is ProperNoun)
                                          select p) {
                    var pronounsInDO = from pn in vp.DirectObjects
                                       let pos = pn as PossessivePronoun
                                       where pos != null
                                       select pos;
                    var pronounsInIO = from pn in vp.IndirectObjects
                                       let pos = pn as PossessivePronoun
                                       where pos != null
                                       select pos;
                    var propernounsInSubject = from propn in vp.Subjects
                                               let pos = propn as ProperNoun
                                               where pos != null
                                               select pos;
                    foreach (var pronoun in pronounsInDO.Concat(pronounsInIO)) {
                        foreach (var propernoun in propernounsInSubject) {
                            pronoun.PossessesFor = propernoun;
                        }
                    }
                }
            }
        }


    }

}
