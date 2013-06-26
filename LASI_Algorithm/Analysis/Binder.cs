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
        public static Task[] GetBindingTasksForDocument(Document doc) {
            return new[]{
                  Task.Run(() => PerformAttributeNounPhraseBinding(doc.Sentences)),
                  Task.Run(() => PerformIntraPhraseBinding(doc.Phrases)),
                  Task.Run(() => PerformSVOBinding(doc.Sentences)),
                  Task.Run(() => PerformPronounBinding(doc))};
        }
        public static void Bind(Document doc) {

            PerformAttributeNounPhraseBinding(doc.Sentences);
            PerformIntraPhraseBinding(doc.Phrases);
            PerformSVOBinding(doc.Sentences);
            PerformPronounBinding(doc);

        }



        #region Private Static Methods

        private static void PerformAttributeNounPhraseBinding(IEnumerable<Sentence> sentences) {
            sentences.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
               .Where(s => s.Paragraph.ParagraphKind == ParagraphKind.Default)
               .ForAll(s => new AttributiveNounPhraseBinder(s));
        }
        private static void PerformSVOBinding(IEnumerable<Sentence> sentences) {
            try {
                sentences
                    .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                    .Where(s => s.Paragraph.ParagraphKind == ParagraphKind.Default && s.Phrases.GetVerbPhrases().Any())
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
