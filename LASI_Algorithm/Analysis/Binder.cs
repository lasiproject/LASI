using LASI.Algorithm.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities.TypedSwitch;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Analysis;

namespace LASI.Algorithm.Binding
{
    public static class Binder
    {
        public static IEnumerable<ProcessingTask> GetBindingTasksForDocument(Document document) {
            return new[]{
                new ProcessingTask(document, Task.Run(() => PerformAttributePhraseBinding(document.Sentences)),
                    string.Format("{0}: Binding Attributives", document.FileName),
                    string.Format("{0}: Bound Attributives", document.FileName), 5),
                new ProcessingTask(document, Task.Run(() => PerformIntraPhraseBinding(document.Phrases)),
                    string.Format("{0}: Decomposing Phrasals", document.FileName),
                    string.Format("{0}: Decomposed Phrasals", document.FileName), 5),
                new ProcessingTask(document, Task.Run(() => PerformSVOBinding(document.Sentences)),
                    string.Format("{0}: Analyzing Verbal Relationships", document.FileName),
                    string.Format("{0}: Analyzed Verbal Relationships", document.FileName), 5), 
                new ProcessingTask(document, Task.Run(() => PerformPronounBinding(document)),
                    string.Format("{0}: Abstracting References", document.FileName),
                    string.Format("{0}: Abstracted References", document.FileName), 5),
            };
        }
        public static void Bind(Document doc) {

            PerformAttributePhraseBinding(doc.Sentences);
            PerformIntraPhraseBinding(doc.Phrases);
            PerformSVOBinding(doc.Sentences);
            PerformPronounBinding(doc);

        }
        public static async Task BindAsync(Document doc) {
            await Task.Run(() => Bind(doc));
        }



        #region Private Static Methods

        private static void PerformAttributePhraseBinding(IEnumerable<Sentence> sentences) {

            sentences.AsParallel()
                .WithDegreeOfParallelism(Concurrency.CurrentMax)
                .ForAll(s => new AttributiveNounPhraseBinder().Bind(s));
        }
        private static void PerformSVOBinding(IEnumerable<Sentence> sentences) {
            try {
                sentences
                    .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)

                    .ForAll(sentence => {
                        try {
                            new SubjectBinder().Bind(sentence);
                        }
                        catch (NullReferenceException) {
                        }
                        try {
                            new ObjectBinder().Bind(sentence);
                        }
                        catch (InvalidStateTransitionException) {
                        }
                        catch (VerblessPhrasalSequenceException) {
                        }
                        catch (InvalidOperationException) {
                        }
                    });
            }
            catch (Exception) {
            }
        }
        private static void PerformIntraPhraseBinding(IEnumerable<Phrase> phrases) {

            phrases
                 .GetNounPhrases()
                 .AsParallel()
                 .WithDegreeOfParallelism(Concurrency.CurrentMax)
                 .ForAll(np => new IntraPhraseWordBinder().Bind(np));
            phrases
                 .GetVerbPhrases()
                 .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                 .ForAll(verbPhrase => new IntraPhraseWordBinder().Bind(verbPhrase));
        }


        private static void PerformPronounBinding(Document document) {
            new PronounBinder().Bind(document);
        }

        #endregion


    }

}
