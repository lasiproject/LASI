using LASI.Algorithm.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities.TypedSwitch;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Analysis.Binding;

namespace LASI.Algorithm.Analysis
{
    public static class Binder
    {
        public static async Task BindAsync(LASI.Algorithm.DocumentConstructs.Document doc)
        {
            await Task.Run(() => Bind(doc));
        }
        public static void Bind(Document doc)
        {
            try
            {
                PerformIntraPhraseBinding(doc);
                PerformAttributeNounPhraseBinding(doc);
                PerformSVOBinding(doc);
                PerformPronounBinding(doc);
            }
            catch (VerblessPhrasalSequenceException)
            {
            }
            catch (InvalidOperationException)
            {
            }
        }

        private static void PerformPronounBinding(Document doc) {
             doc.Sentences
                 .Where(s => s.ParentParagraph.ParagraphKind == ParagraphKind.Default)
                    .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                    .ForAll(
                    s => new PronounBinder().Bind(doc));
        }


        #region Private Static Methods

        private static void PerformAttributeNounPhraseBinding(Document doc)
        {
            doc.Sentences
              .Where(s => s.ParentParagraph.ParagraphKind == ParagraphKind.Default)
                .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                .ForAll(
                s =>
                {
                    var attributiveBinder = new AttributiveNounPhraseBinder(s);
                });
        }
        private static void PerformSVOBinding(Document doc)
        {
            try
            {
                doc.Sentences
                 .Where(s => s.ParentParagraph.ParagraphKind == ParagraphKind.Default)
                    .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                    .ForAll(
                    s =>
                    {
                        try
                        {
                            new SubjectBinder().Bind(s);
                        }
                        catch (NullReferenceException)
                        {
                        }
                        try
                        {
                            if (s.Phrases.GetVerbPhrases().Count() > 0)
                            {
                                new ObjectBinder().Bind(s);
                            }
                        }
                        catch (InvalidStateTransitionException)
                        {
                        }
                        catch (VerblessPhrasalSequenceException)
                        {
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    });
            }
            catch
            {
            }
        }

        private static void PerformIntraPhraseBinding(Document doc)
        {




            foreach (var r in doc.Phrases)
            {
                var wordBinder = new InterPhraseWordBinding();
                new LASI.Utilities.TypedSwitch.Switch(r)
                .Case<NounPhrase>(np =>
                {
                    wordBinder.IntraNounPhrase(np);
                })
                .Case<VerbPhrase>(vp =>
                {
                    wordBinder.IntraVerbPhrase(vp);
                })
                .Default(a =>
                {
                });
            }

        #endregion
        }
    }

}
