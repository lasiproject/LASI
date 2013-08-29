using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Patternization;
using LASI.Utilities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new[] {
                new ProcessingTask(document, BindAttributivesAsync(document.Sentences),
                    string.Format("{0}: Binding Attributives", document.Name),
                    string.Format("{0}: Bound Attributives", document.Name), 5),
                new ProcessingTask(document,  BindIntraPhraseAsync(document.Phrases),
                    string.Format("{0}: Decomposing Phrasals", document.Name),
                    string.Format("{0}: Decomposed Phrasals", document.Name), 5),
                new ProcessingTask(document, BindSubjectsAndObjectsAsync(document.Sentences),
                    string.Format("{0}: Analyzing Verbal Relationships", document.Name),
                    string.Format("{0}: Analyzed Verbal Relationships", document.Name), 5), 
                new ProcessingTask(document,  BindPronounsAsync(document.Sentences),
                    string.Format("{0}: Abstracting References", document.Name),
                    string.Format("{0}: Abstracted References", document.Name), 5),
            };
        }

        /// <summary>
        ///  Performs all binding procedures on the given Document.
        /// </summary>
        /// <param name="doc">The Document to bind within.</param> 
        public static void Bind(Document doc) {
            BindAttributives(doc.Sentences);
            BindIntraPhrase(doc.Phrases);
            BindSubjectsAndObjects(doc.Sentences);
            BindPronouns(doc.Sentences);
            var results = Destructure
                .MatchMany(doc.Phrases.First())
                .Yield<ILexical>()
                .For<IEntity>(e => e.SubjectOf)
                .For<IVerbal>(e => new AggregateEntity(e.DirectObjects))
                .Always(l => l).Results();
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

        #region Standard Implementations

        private static void BindAdjectivePhrases(IEnumerable<Sentence> sentences) {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max).ForAll(s => AdjectivePhraseBinder.Bind(s));
        }

        private static void BindAttributives(IEnumerable<Sentence> sentences) {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max).ForAll(s => AttributivePhraseBinder.Bind(s));
        }

        private static void BindSubjectsAndObjects(IEnumerable<Sentence> sentences) {
            try {
                sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                    .ForAll(s => {
                        try { new SubjectBinder().Bind(s); } catch (NullReferenceException) { }
                        try { new ObjectBinder().Bind(s); } catch (InvalidStateTransitionException) { } catch (VerblessPhrasalSequenceException) { } catch (InvalidOperationException) { }
                    });
            } catch (Exception e) { Output.WriteLine(e.Message); }
        }

        private static void BindIntraPhrase(IEnumerable<Phrase> phrases) {
            phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .GetNounPhrases().ForAll(np => IntraPhraseWordBinder.Bind(np));
            phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .GetVerbPhrases().ForAll(vp => IntraPhraseWordBinder.Bind(vp));
        }

        private static void BindPronouns(IEnumerable<Sentence> sentences) {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(s => PronounBinder.Bind(s));
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(s => new LASI.Algorithm.Binding.Experimental.ClauseSeperatingMultiBranchingBinder().Bind(s.Words));

        }

        #endregion

        #region Async Implementations

        private static async Task BindIntraPhraseAsync(IEnumerable<Phrase> phrases) {
            await Task.Run(() => BindIntraPhrase(phrases));
        }
        private static async Task BindSubjectsAndObjectsAsync(IEnumerable<Sentence> sentences) {
            await Task.Run(() => BindSubjectsAndObjects(sentences));
        }
        private static async Task BindAttributivesAsync(IEnumerable<Sentence> sentences) {
            await Task.Run(() => BindAttributives(sentences));
        }
        private static async Task BindPronounsAsync(IEnumerable<Sentence> sentences) {
            await Task.Run(() => BindPronouns(sentences));
        }

        #endregion

        #endregion


    }

}
