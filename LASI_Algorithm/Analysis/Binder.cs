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
    /// <summary>
    /// Provides static acess to a comprehensive set of binding operations which are applicable to a document.
    /// </summary>
    public static class Binder
    {
        /// <summary>
        /// Gets an ordered collection of ProcessingTask objects which correspond to the steps required to Bind the given document.
        /// Each ProcessingTask contains a Task property which, when awaited will perform a step of the Binding process.
        /// </summary>
        /// <param name="document">The document for which to get the ProcessingTasks for Binding.</param>
        /// <returns>An ordered collection of ProcessingTask objects which correspond to the steps required to Bind the given document.
        /// </returns>
        /// <remarks>
        /// ProcessingTasks returned by this method may be run in an arbitrary order.
        /// However, to ensure the consistency/determinism of the Binding process, it is recommended that they be executed (awaited) in the order
        /// in which they are hereby returned.
        /// </remarks>
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
        /// <summary>
        /// Asynchronously performs all binding procedures on the given Document.
        /// </summary>
        /// <param name="doc">The Document to bind within.</param> 
        public static void Bind(Document doc) {

            PerformAttributePhraseBinding(doc.Sentences);
            PerformIntraPhraseBinding(doc.Phrases);
            PerformSVOBinding(doc.Sentences);
            PerformPronounBinding(doc);

        }
        /// <summary>
        /// Asynchronously performs all binding procedures on the given Document.
        /// </summary>
        /// <param name="doc">The Document to bind within.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
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
