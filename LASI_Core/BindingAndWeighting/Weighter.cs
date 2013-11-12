using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.Utilities;
using LASI.Core.Patternization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Provides static access to a comprehensive set of weighting operations which are applicable to a document.
    /// </summary>
    public static class Weighter
    {
        /// <summary>
        /// Gets an ordered collection of ProcessingTask objects which correspond to the steps required to Weight the given document.
        /// Each ProcessingTask contains a Task property which, when awaited will perform a step of the Weighting process.
        /// </summary>
        /// <param name="document">The document for which to get the ProcessingTasks for Weighting.</param>
        /// <returns>An ordered collection of ProcessingTask objects which correspond to the steps required to Weight the given document.
        /// </returns>
        /// <remarks>
        /// ProcessingTasks returned by this method may be run in an arbitrary order.
        /// However, to ensure the consistency/determinism of the Weighting process, it is recommended that they be executed (awaited) in the order
        /// in which they are hereby returned.
        /// </remarks>
        public static IEnumerable<ProcessingTask> GetWeightingTasks(this DocumentStructures.Document document) {
            yield return new ProcessingTask(() => WeightByLiteralFrequency(document.Words),
                string.Format("{0}: Aggregating Literals", document.Name),
                string.Format("{0}: Aggregated Literals", document.Name),
                    23);
            yield return new ProcessingTask(() => WeightByLiteralFrequency(document.Phrases),
                string.Format("{0}: Aggregating Complex Literals", document.Name),
                string.Format("{0}: Aggregated Complex Literals", document.Name),
                11);
            yield return new ProcessingTask(() => ModifyNounWeightsBySynonyms(document),
                string.Format("{0}: Generalizing Nouns", document.Name),
                string.Format("{0}: Generalized Nouns", document.Name),
                15);
            yield return new ProcessingTask(() => ModifyVerbWeightsBySynonyms(document),
                string.Format("{0}: Generalizing Verbs", document.Name),
                string.Format("{0}: Generalized Verbs", document.Name),
                10);
            yield return new ProcessingTask(() => WeightSimilarNounPhrases(document),
                string.Format("{0}: Generalizing Phrases", document.Name),
                string.Format("{0}: Generalized Phrases", document.Name),
                20);
            yield return new ProcessingTask(() => WeightSimilarVerbPhrases(document),
                string.Format("{0}: Generalizing Complex Verbals", document.Name),
                string.Format("{0}: Generalized Complex Verbals", document.Name),
                10);
            yield return new ProcessingTask(() => HackSubjectPropernounImportance(document),
                string.Format("{0}: Focusing Patterns", document.Name),
                string.Format("{0}: Focused Patterns", document.Name),
                6);
            yield return new ProcessingTask(() => NormalizeWeights(document),
                string.Format("{0}: Normalizing Metrics", document.Name),
                string.Format("{0}: Normalized Metrics", document.Name),
                3);
        }

        static void PerformWeightingPhrase(Task phase, string documentSpecificMessageText, double numericValue) {
            var startHandler = StartedWeightingPhase;
            if (startHandler != null) {
                startHandler(new object(), new WeighterProgressUpdateEventArgs {
                    TextualProgressUpdate = documentSpecificMessageText,
                    NumericalProgressUpdate = numericValue
                });
            };
            phase.Wait();
            var endHandler = FinishedWeightingPhase;
            if (endHandler != null) {
                endHandler(new object(), new WeighterProgressUpdateEventArgs {
                    TextualProgressUpdate = documentSpecificMessageText,
                    NumericalProgressUpdate = numericValue
                });
            }
        }



        /// <summary>
        /// Assigns numeric Weights to each elemenet in the given Document.
        /// </summary>
        /// <param name="document">The Document whose elements are to be assigned numeric weights.</param>
        public static void Weight(Document document) {
            Task.WaitAll(document.GetWeightingTasks().Select(t => t.Task).ToArray());
        }
        private static void NormalizeWeights(Document doc) {
            if (doc.Phrases.Any()) {
                var maxWeight = doc.Phrases.Max(p => p.Weight);
                if (maxWeight != 0)
                    foreach (var p in doc.Phrases) {
                        var proportion = p.Weight / maxWeight;
                        proportion *= 100;
                        p.Weight = proportion;
                    }
            }
        }
        private static void ModifyVerbWeightsBySynonyms(Document doc) {
            var verbsToConsider = doc.Words.OfVerb().AsParallel().WithDegreeOfParallelism(Concurrency.Max).WithSubjectOrObject().ToList();
            var groups = from outer in verbsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                         from inner in verbsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                         where outer.IsSynonymFor(inner)
                         group inner by outer;
            groups.ForAll(grp => {
                var increase = grp.Count();
                foreach (var e in grp) {
                    e.Weight += increase;
                }
            });
        }
        /// <summary>
        /// Increase noun weights in a document by abstracting over synonyms
        /// </summary>
        /// <param name="doc">the Document whose noun weights may be modiffied</param>
        private static void ModifyNounWeightsBySynonyms(Document doc) {
            var toConsider = (from e in doc.Words
                                 .AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                 .OfEntity().InSubjectOrObjectRole() //Currently, include only those nouns which exist in relationships with some IVerbal or IPronoun.
                              let result =
                                e.Match().Yield<IEntity>()
                                    ._<Noun>(e)
                                    ._<IReferencer>(r => r.Referent ?? r as IEntity)
                                .Result()
                              where result != null
                              select result).ToList();

            var synonymGroups =
                from outer in toConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                from inner in toConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                where outer.IsSimilarTo(inner)
                group inner by outer into grouped
                select new { SynGroup = grouped, Count = grouped.Count() };

            synonymGroups.ForAll(grp => {
                var increase = grp.Count;
                foreach (var e in grp.SynGroup) {
                    e.Weight += increase;
                }
            });
        }
        private static void WeightByLiteralFrequency(IEnumerable<ILexical> syntacticElements) {
            var byTypeAndText = from e in syntacticElements.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                group e by new { e.Type, e.Text } into g
                                select new { Increase = g.Count(), Elements = g.ToList() };
            byTypeAndText.ForAll(g => g.Elements.ForEach(e => e.Weight += g.Increase));

        }
        /// <summary>
        /// For each noun parent in a document that is similar to another noun parent, increase the weight of that noun
        /// </summary>
        /// <param name="doc">Document containing the componentPhrases to weight</param>
        private static void WeightSimilarNounPhrases(Document doc) {
            //Reify the query source so that it may be queried to form a full self join (cartesian product with itself.
            // in the two subsequent from clauses both query the reified collection in paralllel.
            var npsToConsider = doc.Phrases
                .AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .OfNounPhrase()
                .InSubjectOrObjectRole()
                .ToList();
            var nps = from outer in npsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      from inner in npsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      where inner.IsAliasFor(outer) || inner.IsSimilarTo(outer)
                      group inner by outer into grouped
                      select new { WeightIncrease = grouped.Count() * 0.5, Elements = grouped };
            nps.ForAll(grp => { foreach (var e in grp.Elements) { e.Weight += grp.WeightIncrease; } });
        }
        private static void WeightSimilarVerbPhrases(Document doc) {
            //Reify the query source so that it may be queried to form a full self join (cartesian product with itself.
            // in the two subsequent from clauses both query the reified collection in paralllel.
            var vpsToConsider = doc.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max).OfVerbPhrase().WithSubjectOrObject().ToList();
            var vps = from outer in vpsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      from inner in vpsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                      where inner.IsSimilarTo(outer)
                      group inner by outer into grouped
                      select new { WeightIncrease = grouped.Count() * 0.5, Elements = grouped };
            vps.ForAll(grp => { foreach (var e in grp.Elements) { e.Weight += grp.WeightIncrease; } });

        }
        private static void WeightSimilarEntities(Document doc) {
            var entities = doc.GetEntities().ToList();

            doc.GetEntities().AsParallel().WithDegreeOfParallelism(Concurrency.Max).ForAll(outer => {
                var groups = from inner in entities.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                             where inner.IsAliasFor(outer) || inner.IsSimilarTo(outer)
                             group inner by outer;
                foreach (var group in groups) {
                    var weightIncrease = group.Count() * .5;
                    foreach (var inner in group) {
                        inner.Weight += weightIncrease;
                    }
                }
            });

        }
        private static void HackSubjectPropernounImportance(Document doc) {
            doc.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .OfNounPhrase()
                .Where(np => np.Words.Any(w => w is ProperNoun))
                .ForAll(np => np.Weight *= 2);

        }
        private static void OldNormalizationProcedure(Document doc) {
            double TotPhraseWeight = 0.0;
            double MaxWeight = 0.0;
            int NonZeroWghts = 0;
            foreach (var w in doc.Phrases) {
                TotPhraseWeight += w.Weight;

                if (w.Weight > 0)
                    NonZeroWghts++;

                if (w.Weight > MaxWeight)
                    MaxWeight = w.Weight;
            }
            if (NonZeroWghts != 0) {//Caused a devide by zero exception if document was empty.
                var ratio = 100 / MaxWeight;

                foreach (var p in doc.Phrases) {
                    p.Weight = Math.Round(p.Weight * ratio, 3);
                }
            }
        }

        #region Events
        public static event EventHandler<WeighterProgressUpdateEventArgs> StartedWeightingPhase;
        public static event EventHandler<WeighterProgressUpdateEventArgs> FinishedWeightingPhase;
        #endregion Events

    }

    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class WeighterProgressUpdateEventArgs : EventArgs
    {
        internal WeighterProgressUpdateEventArgs() {
            TextualProgressUpdate = "";
            NumericalProgressUpdate = 0;
        }
        public double NumericalProgressUpdate { get; internal set; }
        public string TextualProgressUpdate { get; internal set; }
    }

}