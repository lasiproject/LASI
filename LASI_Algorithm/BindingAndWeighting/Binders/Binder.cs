using LASI.Algorithm.Binding;
using LASI.Algorithm.Binding.Experimental;
using LASI.Algorithm.DocumentStructures;
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
        public static IEnumerable<ProcessingTask> GetBindingTasks(this Document document)
        {

            yield return new ProcessingTask(() => BindAttributives(document.Sentences),
                    string.Format("{0}: Binding Attributives", document.Name),
                    string.Format("{0}: Bound Attributives", document.Name), 5);
            yield return new ProcessingTask(() => BindIntraPhrase(document.Phrases),
                    string.Format("{0}: Decomposing Phrasals", document.Name),
                    string.Format("{0}: Decomposed Phrasals", document.Name), 5);
            yield return new ProcessingTask(() => BindSubjectsAndObjects(document.Sentences),
                    string.Format("{0}: Analyzing Verbal Relationships", document.Name),
                    string.Format("{0}: Analyzed Verbal Relationships", document.Name), 5);
            yield return new ProcessingTask(() => BindPronouns(document.Sentences),
                    string.Format("{0}: Abstracting References", document.Name),
                    string.Format("{0}: Abstracted References", document.Name), 5);
        }

        private static void PreBind(IEnumerable<Paragraph> enumerable)
        {
            foreach (var p in enumerable) {
                LASI.Algorithm.BindingAndWeighting.Binders.Experimental.PreBinder.BindPairedDelimiters(p);
            }
        }

        /// <summary>
        ///  Performs all binding procedures on the given Document.
        /// </summary>
        /// <param name="document">The Document to bind within.</param> 
        public static void Bind(Document document)
        {
            Task.WaitAll(document.GetBindingTasks().Select(t => t.Task).ToArray());
        }


        #region Private Static Methods

        #region Standard Implementations

        private static void BindAdjectivePhrases(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max).ForAll(s => AdjectivePhraseBinder.Bind(s));
        }

        private static void BindAttributives(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max).ForAll(s => AttributivePhraseBinder.Bind(s));
        }

        private static void BindSubjectsAndObjects(IEnumerable<Sentence> sentences)
        {
            try {
                sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                    .ForAll(s => {
                        try { new SubjectBinder().Bind(s); }
                        catch (NullReferenceException e) { Output.WriteLine(e.Message); }
                        try { new ObjectBinder().Bind(s); }
                        catch (InvalidStateTransitionException e) { Output.WriteLine(e.Message); }
                        catch (VerblessPhrasalSequenceException e) { Output.WriteLine(e.Message); }
                        catch (InvalidOperationException e) { Output.WriteLine(e.Message); }
                    });
            }
            catch (Exception e) { Output.WriteLine(e.Message); }
        }

        private static void BindIntraPhrase(IEnumerable<Phrase> phrases)
        {
            phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .GetNounPhrases().ForAll(np => IntraPhraseWordBinder.Bind(np));
            phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .GetVerbPhrases().ForAll(vp => IntraPhraseWordBinder.Bind(vp));
        }

        private static void BindPronouns(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(s => PronounBinder.Bind(s));
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(s => ClauseSeperatingMultiBranchingBinder.Bind(s.Words));

        }

        #endregion


        #endregion


    }

}
