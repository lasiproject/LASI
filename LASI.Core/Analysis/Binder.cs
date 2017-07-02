using LASI.Core.Binding;
using LASI.Core.Binding.Experimental;
using LASI.Core.Configuration;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using LASI.Core.Analysis.Binding;

namespace LASI.Core
{
    /// <summary>
    /// Provides static access to a comprehensive set of binding operations which are applicable to a document.
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
                $"{document.Name}: Binding Attributives",
                $"{document.Name}: Bound Attributives", 4);
            yield return new ProcessingTask(() => BindIntraPhrase(document.Phrases),
                $"{document.Name}: Decomposing Phrasals",
                $"{document.Name}: Decomposed Phrasals", 4);
            yield return new ProcessingTask(() => BindSubjectsAndObjects(document.Sentences),
                $"{document.Name}: Analyzing Verbal Relationships",
                $"{document.Name}: Analyzed Verbal Relationships", 4);
            yield return new ProcessingTask(() => BindAdjectivePhrases(document.Sentences),
                $"{document.Name}: Analyzing Verbal Relationships",
                $"{document.Name}: Analyzed Adjectival Relationships", 4);
            yield return new ProcessingTask(() => BindPronouns(document.Sentences),
                $"{document.Name}: Abstracting References",
                $"{document.Name}: Abstracted References", 4);
        }

        #region Private Static Methods

        private static void BindAdjectivePhrases(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(AdjectivePhraseBinder.Bind);
        }

        private static void BindAttributives(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(AttributivePhraseBinder.Bind);
        }

        private static void BindSubjectsAndObjects(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(sentence =>
                {
                    try
                    {
                        new SubjectBinder().Bind(sentence);
                    }
                    catch (Exception e) when (e is NullReferenceException || e is VerblessPhrasalSequenceException)
                    {
                        e.Log();
                    }
                    try
                    {
                        new ObjectBinder().Bind(sentence);
                    }
                    catch (Exception e) when (e is InvalidStateTransitionException || e is VerblessPhrasalSequenceException || e is InvalidOperationException)
                    {
                        e.Log();
                    }
                    new IntraSentenceIDescriptorToVerbalSubjectBinder().Bind(sentence);
                });
        }

        //private static void MatchSentences(IEnumerable<Sentence> sentences)
        //{
        //    sentences.AsParallel()
        //        .WithDegreeOfParallelism(Concurrency.Max)
        //        .ForAll(DeclarativeBinder.Bind);
        //}


        private static void BindIntraPhrase(IEnumerable<Phrase> phrases)
        {
            phrases.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .OfNounPhrase()
                .ForAll(IntraPhraseWordBinder.Bind);
            phrases.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .OfVerbPhrase()
                .ForAll(IntraPhraseWordBinder.Bind);
        }

        private static void BindPronouns(IEnumerable<Sentence> sentences)
        {
            sentences.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .ForAll(PronounBinder.Bind);
            sentences.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .Select(sentence => sentence.Words)
                .ForAll(AdaptivePronounBinder.Bind);
        }

        #endregion


    }
}
