using LASI;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Lookup;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// Attempts to bind pronouns to the entities they refer to.
    /// </summary>
    public class PronounBinder
    {
        /// <summary>
        /// Attempts to perform pronoun binding over the entirety of the given Document.
        /// </summary>
        /// <param name="document">The document over which to perform pronoun bindind.</param>
        public void Bind(Document document) {
            BindPosessivePronouns(document);
            //ReifyContextualNounPhrasesAsPronounPhrases(document.Phrases.GetNounPhrases());
        }


        /// <summary>
        /// Bind posessive pronouns located in the objects of a sentence to the proper noun in the subject of that sentence. 
        /// Example Sentence that this applies to:
        /// "LASI binds it's pronouns."
        /// Pronoun "it's" binds to the proper noun "LASI"
        /// </summary>
        /// <param name="doc">Document for analysis</param>
        private void BindPosessivePronouns(Document doc) { //Aluan Says: Beautiful function man.

            var vps = from vp in doc.Phrases.GetVerbPhrases().WithSubject(s => s is ProperNoun)
                      where vp.DirectObjects.Any(o => o is PossessivePronoun) || vp.IndirectObjects.Any(o => o is PossessivePronoun)
                      select vp;
            //            foreach (var verbPhrase in vps) {
            vps.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
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
        private void ReifyContextualNounPhrasesAsPronounPhrases(IEnumerable<NounPhrase> candidatesNounPhrases) {
            var toTransform = from np in candidatesNounPhrases
                              where np.Words.Count() < 4
                              let det = np.GetLeadingDeterminer()
                              where det != null && det.DeterminerKind == DeterminerKind.Definite
                              where np.Words.Last() is GenericNoun
                              select np;
            foreach (var np in toTransform) {
                var temporaryReference = np;
            }
            BindToReferencePoints(toTransform);
        }

        private static void BindToReferencePoints(IEnumerable<NounPhrase> toTransform) {

            toTransform
                .GetNounPhrases()
                .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(contextualPro => {
                    var target = contextualPro
                    .Document
                    .Phrases
                    .TakeWhile(p => p != contextualPro)
                    .Reverse()
                    .GetNounPhrases()
                    .FirstOrDefault(np => np.IsSimilarTo(contextualPro.Words.GetNouns().Last()));
                    if (target != null)
                        AliasDictionary.DefineAlias(contextualPro, target);
                });
        }

    }

}
