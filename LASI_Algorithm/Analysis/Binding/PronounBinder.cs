using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm;
using LASI.Utilities;
using LASI.Algorithm.LexicalStructures;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.Algorithm.Analysis.Binding
{
    public class PronounBinder
    {
        public void Bind(Document document) {



            possessivePronounBinderWithinSentence(document);

        }


        /// <summary>
        /// Bind posessive pronouns located in the objects of a sentence to the proper noun in the subject of that sentence. 
        /// Example Sentence that this applies to:
        /// "LASI binds it's pronouns."
        /// Pronoun "it's" binds to the proper noun "LASI"
        /// </summary>
        /// <param name="doc">Document for analysis</param>
        public void possessivePronounBinderWithinSentence(Document doc) { //Aluan Says: Beautiful function man.
            foreach (var s in doc.Sentences) {
                foreach (VerbPhrase vp in from VerbPhrase p in s.Phrases.GetVerbPhrases()
                                          where p.DirectObjects.Any() || p.IndirectObjects.Any()
                                          where p.DirectObjects.Any((IEntity dirobj) => dirobj is PossessivePronoun) ||
                                                p.IndirectObjects.Any((IEntity indirobj) => indirobj is PossessivePronoun)
                                          where p.BoundSubjects.Any((IEntity subject) => subject is ProperNoun)
                                          select p) {
                    var pronounsInDO = from pn in vp.DirectObjects
                                       let pos = pn as PossessivePronoun
                                       where pos != null
                                       select pos;
                    var pronounsInIO = from pn in vp.IndirectObjects
                                       let pos = pn as PossessivePronoun
                                       where pos != null
                                       select pos;
                    var propernounsInSubject = from propn in vp.BoundSubjects
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
#region Deprecated Bind Implmenttation

//foreach (var g in documennt.Paragraphs) {
//    NounPhrase lastTarg = null;
//    foreach (var f in g.Phrases.GetPhrasesAfter(lastTarg ?? g.Phrases.First()).GetNounPhrases().Where(n => !(n is PronounPhrase))) {
//        var pairs = from i in g.Phrases.GetPhrasesAfter(f).GetPronounPhrases()

//                    select new {
//                        nps = f,
//                        pro = i
//                    };
//        foreach (var n in pairs.Where(r => r.pro.PronounKind == PronounKind.GenderNeurtral || r.pro.PronounKind == PronounKind.Inanimate))

//        //.GetNounPhrases().Where(
//        //   )

//    {

//            if (n.pro != null) {
//                n.nps.BindPronoun(n.pro);
//                n.pro.BindToIEntity(n.nps);
//            }

//        }
//        lastTarg = f;
//    }
//}

#endregion