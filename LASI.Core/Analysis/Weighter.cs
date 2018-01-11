using LASI.Core.Configuration;
using LASI.Utilities.Specialized.Enhanced.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public static IEnumerable<ProcessingTask> GetWeightingTasks(this Document document)
        {
            var name = document.Name;
            yield return new ProcessingTask(() => WeightByLiteralFrequency(document.Words),
                name + ": Aggregating Literals", name + ": Aggregated Literals", 23);
            yield return new ProcessingTask(() => WeightByLiteralFrequency(document.Phrases),
                name + ": Aggregating Complex Literals", name + ": Aggregated Complex Literals", 11);
            yield return new ProcessingTask(() => WeightSimilarNouns(document),
                name + ": Generalizing Nouns", name + ": Generalized Nouns", 15);
            yield return new ProcessingTask(() => WeightSimilarVerbs(document),
                name + ": Generalizing Verbs", name + ": Generalized Verbs", 10);
            yield return new ProcessingTask(() => WeightSimilarNounPhrases(document),
                name + ": Generalizing Phrases", name + ": Generalized Phrases", 20);
            yield return new ProcessingTask(() => WeightSimilarVerbPhrases(document),
                name + ": Generalizing Complex Verbals", name + ": Generalized Complex Verbals", 10);
            yield return new ProcessingTask(() => HackSubjectPropernounImportance(document),
                name + ": Focusing Patterns", name + ": Focused Patterns", 6);
            yield return new ProcessingTask(() => NormalizeWeights(document),
                name + ": Normalizing Metrics", name + ": Normalized Metrics", 3);
        }
        /// <summary>
        /// Assigns numeric Weights to each element in the given Document.
        /// </summary>
        /// <param name="document">The Document whose elements are to be assigned numeric weights.</param>
        public static void Weight(Document document) => Task.WaitAll(document.GetWeightingTasks().Select(t => t.Task).ToArray());
        /// <summary>
        /// Assigns numeric Weights to each element in the given Document.
        /// </summary>
        /// <param name="document">The Document whose elements are to be assigned numeric weights.</param>
        /// <param name="cancellationToken"></param>
        public static async Task WeightAsync(Document document, CancellationToken cancellationToken = default)
        {

            await new ProcessingTask(() => { Console.Write("X"); },
                      initializationMessage: $"{document.Name}: Abstracting References",
                      completionMessage: $"{document.Name}: Abstracted References",
                      percentWorkRepresented: 5);


            cancellationToken.ThrowIfCancellationRequested();

            await Task.WhenAll(document.GetWeightingTasks().Select(async x => await x.Task));
        }

        static void GroupAndWeight<TLexical>(IEnumerable<TLexical> toConsider,
            Func<TLexical, TLexical, Similarity> correlate, double scaleBy) where TLexical : class, ILexical
        {
            var groups =
                from outer in toConsider
                    .AsParallel()
                    .WithDegreeOfParallelism(Concurrency.Max)
                from inner in toConsider
                    .ToList()
                    .AsParallel()
                    .WithDegreeOfParallelism(Concurrency.Max)
                where !ReferenceEquals(inner, outer) || correlate(inner, outer)
                group inner by outer
                into grouped
                select grouped.ToList();

            groups.ForAll(elements =>
            {
                void scaleWeight<T>(T element) where T : TLexical => element.Weight += scaleBy * elements.Count;
                elements.ForEach(scaleWeight);
            });
        }
        static void HackSubjectPropernounImportance(IReifiedTextual source) => source.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .OfNounPhrase()
                .Where(nounPhrase => nounPhrase.Words.OfProperNoun().Any())
                .ForAll(nounPhrase => nounPhrase.Weight *= 2);
        static void NormalizeWeights(IReifiedTextual source)
        {
            if (source.Phrases.Any())
            {
                var maxWeight = source.Phrases.Max(p => p.Weight);
                if ((int)maxWeight != 0)
                {
                    foreach (var phrase in source.Phrases)
                    {
                        phrase.Weight = phrase.Weight / maxWeight * 100;
                    }
                }
            }
        }
        static void WeightByLiteralFrequency(IEnumerable<ILexical> syntacticElements) => syntacticElements.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .GroupBy(lexical => new
                {
                    Type = lexical.GetType(),
                    lexical.Text
                }).Select(Enumerable.ToList)
                .ForAll(elements => elements.ForEach(e => e.Weight += elements.Count));
        /// <summary>
        /// For each noun parent in a document that is similar to another noun parent, increase the weight of that noun
        /// </summary>
        /// <param name="source">Document containing the componentPhrases to weight</param>
        static void WeightSimilarNounPhrases(IReifiedTextual source)
        {
            //Reify the query source so that it may be queried to form a full self join (Cartesian product with itself.
            // in the two subsequent from clauses both query the reified collection in parallel.
            var toConsider = source.Phrases
                .OfNounPhrase()
                .InSubjectOrObjectRole().ToList();
            GroupAndWeight(toConsider, Lexicon.IsSimilarTo, scaleBy: 0.5);
        }
        /// <summary>
        /// Increase noun weights in a document by abstracting over synonyms
        /// </summary>
        /// <param name="source">the Document whose noun weights may be modified</param>
        static void WeightSimilarNouns(IReifiedTextual source)
        {
            var toConsider = from e in source.Words
                                 //.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                 .OfEntity()
                                 .InSubjectOrObjectRole() //Currently, include only those nouns which exist in relationships with some IVerbal or IReferencer.
                             select e.Match()
                                 .When((IReferencer r) => r.RefersTo != null)
                                 .Then((IReferencer r) => r.RefersTo)
                                 .Case((IEntity x) => x.Lift().ToAggregate()).Result()
                             into y
                             where y != null
                             select y;
            GroupAndWeight(toConsider, Lexicon.IsSimilarTo, scaleBy: 1);
        }
        static void WeightSimilarVerbPhrases(IReifiedTextual source)
        {
            //Reify the query source so that it may be queried to form a full self join (Cartesian product with itself.
            // in the two subsequent from clauses both query the reified collection in parallel.
            var toConsider = source.Phrases.OfVerbPhrase().WithSubjectOrObject();
            GroupAndWeight(toConsider, Lexicon.IsSimilarTo, scaleBy: 0.5);
        }
        static void WeightSimilarVerbs(IReifiedTextual source)
        {
            var toConsider = source.Words.OfVerb().WithSubjectOrObject();
            GroupAndWeight(toConsider, Lexicon.IsSimilarTo, scaleBy: 1);
        }
    }
}