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
        public void Bind(Document document) {
            //ReifyContextualNounPhrasesAsPronounPhrases(document.Phrases.GetNounPhrases());
            BindPosessivePronouns(document);
        }

        private void ReifyContextualNounPhrasesAsPronounPhrases(IEnumerable<NounPhrase> candidatesNounPhrases) {
            var toTransform = from np in candidatesNounPhrases
                              where np.Words.Count() < 4
                              let det = np.GetLeadingDeterminer()
                              where det != null && det.DeterminerKind == DeterminerKind.Definite
                              where np.Words.Last() is GenericNoun
                              select np;
            foreach (var np in toTransform) {
                var temporaryReference = np;
                PronounPhrase.TransformNounPhraseToPronounPhrase(ref temporaryReference);
            }
            BindToReferencePoints(toTransform);
        }

        private static void BindToReferencePoints(IEnumerable<NounPhrase> toTransform) {

            toTransform
                .GetPronounPhrases()
                .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                .ForAll(contextualPro => {
                    contextualPro.BindToEntity(contextualPro
                        .Document
                        .Phrases
                        .TakeWhile(p => p != contextualPro)
                        .Reverse()
                        .GetNounPhrases()
                        .FirstOrDefault(np => {
                            return np.IsAliasFor(contextualPro.Words.GetNouns().Last());
                        }));
                });
        }
        /// <summary>
        /// Bind posessive pronouns located in the objects of a sentence to the proper noun in the subject of that sentence. 
        /// Example Sentence that this applies to:
        /// "LASI binds it'subject pronouns."
        /// Pronoun "it'subject" binds to the proper noun "LASI"
        /// </summary>
        /// <param name="doc">Document for analysis</param>
        private void BindPosessivePronouns(Document doc) { //Aluan Says: Beautiful function man.

            var vps = from vp in doc.Phrases.GetVerbPhrases().WithSubject(s => s is ProperNoun)
                      where vp.DirectObjects.Any(o => o is PossessivePronoun) || vp.IndirectObjects.Any(o => o is PossessivePronoun)
                      select vp;
            //            foreach (var verbPhrase in vps) {
            vps.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
            .ForAll(verbPhrase => {
                var pronounsInDO = from pn in verbPhrase.DirectObjects
                                   let pos = pn as PossessivePronoun
                                   where pos != null
                                   select pos;
                var pronounsInIO = from pn in verbPhrase.IndirectObjects
                                   let pos = pn as PossessivePronoun
                                   where pos != null
                                   select pos;
                var propernounsInSubject = from propn in verbPhrase.Subjects
                                           let pos = propn as ProperNoun
                                           where pos != null
                                           select pos;
                foreach (var pronoun in pronounsInDO.Concat(pronounsInIO)) {
                    //.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax).ForAll(pronoun => {
                    foreach (var propernoun in propernounsInSubject) {
                        pronoun.PossessesFor = propernoun;
                    }
                };
            });

        }


    }

}
