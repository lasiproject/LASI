using LASI.Algorithm.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities.TypedSwitch;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.Algorithm.Binding
{
    public static class Binder
    {
        public static async Task BindAsync(LASI.Algorithm.DocumentConstructs.Document doc) {
            await Task.Run(() => Bind(doc));
        }
        public static void Bind(Document doc) {
            try {
                PerformAttributeNounPhraseBinding(doc);
                //PerformPrepositionalPreBinding(doc);
                PerformIntraPhraseBinding(doc);
                PerformSVOBinding(doc);
                PerformPronounBinding(doc);
            }
            catch (VerblessPhrasalSequenceException) {
            }
            catch (InvalidOperationException) {
            }
        }

        //private static void PerformPrepositionalPreBinding(Document doc) {
        //    doc.Sentences
        //        .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
        //        .ForAll(sentence => {
        //            foreach (var prepPhrase in sentence.Phrases.GetPrepositionalPhrases()) {
        //                prepPhrase.OnLeftSide = prepPhrase.PreviousPhrase.Sentence == prepPhrase.Sentence ? prepPhrase.PreviousPhrase : null;
        //                prepPhrase.OnRightSide = prepPhrase.NextPhrase.Sentence == prepPhrase.Sentence ? prepPhrase.NextPhrase : null;
        //            }
        //        });
        //}

        #region Private Static Methods

        private static void PerformAttributeNounPhraseBinding(Document doc) {
            doc.Sentences
                .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                .Where(s => s.Paragraph.ParagraphKind == ParagraphKind.Default)
                .ForAll(s => new AttributiveNounPhraseBinder(s));
        }
        private static void PerformSVOBinding(Document doc) {
            try {
                doc.Sentences
                    .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                    .Where(s => s.Paragraph.ParagraphKind == ParagraphKind.Default)
                    .ForAll(sentence => {
                        try {
                            new SubjectBinder().Bind(sentence);
                        }
                        catch (NullReferenceException) {
                        }
                        try {
                            if (sentence.Phrases.GetVerbPhrases().Any()) {
                                new ObjectBinder().Bind(sentence);
                            }
                        }
                        catch (InvalidStateTransitionException) {
                        }
                        catch (VerblessPhrasalSequenceException) {
                        }
                        catch (InvalidOperationException) {
                        }
                    });
            }
            catch {
            }
        }
        private static void PerformIntraPhraseBinding(Document doc) {

            doc.Phrases
                .GetNounPhrases()
                .AsParallel()
                .WithDegreeOfParallelism(Concurrency.CurrentMax)
                .ForAll(np => new IntraPhraseWordBinder().Bind(np));
            doc.Phrases
                .GetVerbPhrases()
                .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                .ForAll(verbPhrase => new IntraPhraseWordBinder().Bind(verbPhrase));
        }


        private static void PerformPronounBinding(Document doc) {
            doc.Sentences
                .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                .Where(s => s.Paragraph.ParagraphKind == ParagraphKind.Default)
                .ForAll(s => new PronounBinder().Bind(doc));
        }

        #endregion
    }

}
