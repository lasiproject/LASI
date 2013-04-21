using LASI.Algorithm.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities.TypedSwitch;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis
{
    public static class Binder
    {
        public static async Task BindAsync(Document doc) {
            await Task.Run(() => Bind(doc));
        }
        public static void Bind(Document doc) {
            try {
                PerformIntraPhraseBinding(doc);
                PerformAttributeNounPhraseBinding(doc);
                PerformSVOBinding(doc);
            } catch (VerblessPhrasalSequenceException) {
            } catch (InvalidOperationException) {
            }
        }


        #region Private Static Methods

        private static void PerformAttributeNounPhraseBinding(Document doc) {
            doc.Sentences.AsParallel().ForAll(s => {
                var attributiveBinder = new AttributiveNounPhraseBinder(s);
            });
        }
        private static void PerformSVOBinding(Document doc) {
            try {
                doc.Sentences.AsParallel().ForAll(s => {

                    try {
                        new SubjectBinder().Bind(s);
                    } catch (NullReferenceException) {
                    }
                    try {
                        if (s.Phrases.GetVerbPhrases().Count() > 0) {
                            new ObjectBinder().Bind(s);
                        }
                    } catch (InvalidStateTransitionException) {
                    } catch (VerblessPhrasalSequenceException) {
                    }
                });
            } catch {
            }
        }

        private static void PerformIntraPhraseBinding(Document doc) {


            var phrases = from r in doc.Phrases.AsParallel()
                          select r;
            phrases.ForAll(r => {
                var wordBinder = new InterPhraseWordBinding();
                new LASI.Utilities.TypedSwitch.Switch(r)
                .Case<NounPhrase>(np => {
                    wordBinder.IntraNounPhrase(np);
                })
                .Case<VerbPhrase>(vp => {
                    wordBinder.IntraVerbPhrase(vp);
                })
                .Default(a => {
                });
            });
        }

        #endregion
    }

}
