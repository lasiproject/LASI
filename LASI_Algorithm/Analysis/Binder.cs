using LASI.Algorithm.Analysis.Binding;
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
        public static void Bind(Document doc) {
            PerformIntraPhraseBinding(doc);
            PerformAttributeNounPhraseBinding(doc);
            PerformSVOBinding(doc);
        }


        #region Private Static Methods

        private static void PerformAttributeNounPhraseBinding(Document doc) {
            doc.Sentences.AsParallel().ForAll(s => {
                var attributiveBinder = new AttributiveNounPhraseBinder(s);
            });
        }
        private static void PerformSVOBinding(Document doc) {
            doc.Sentences.AsParallel().ForAll(s => {
                var subjectBinder = new SubjectBinder();
                var objectBinder = new ObjectBinder();
                try {
                    subjectBinder.Bind(s);
                } catch (NullReferenceException) {
                }
                try {
                    objectBinder.Bind(s);
                } catch (InvalidStateTransitionException) {
                } catch (VerblessPhrasalSequenceException) {
                }
            });
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
