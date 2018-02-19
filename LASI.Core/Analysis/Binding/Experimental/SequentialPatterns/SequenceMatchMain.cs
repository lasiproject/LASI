using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

// TODO: SequenceMatch like class with result returning case expressions. Probably not add them to the current class. API is large as it is.
namespace LASI.Core.Heuristics.Binding.Experimental.SequentialPatterns
{
    /// <summary>
    /// Represents a binding expression applied to a sequence if lexical constructs.
    /// </summary>
    public partial class SequenceMatch
    {
        #region Constructors

        internal SequenceMatch(IEnumerable<ILexical> sequence) => elements = sequence.ToList();

        internal SequenceMatch(Sentence sentence) : this(sentence.Phrases) { }

        #endregion Constructors

        #region Ignore Clauses

        /// <summary>
        /// Filters elements of the given type out of the sequence before attempting to match any subsequent patterns.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch Ignore<T1>()
            where T1 : class, ILexical
        {
            predicates.Add(v => !(v is T1));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent patterns.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch Ignore<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical
        {
            predicates.Add(v => !(v is T1 || v is T2));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent patterns.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <typeparam name="T3"> The third type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch Ignore<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
        {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent patterns.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <typeparam name="T3"> The third type of element to filter out. </typeparam>
        /// <typeparam name="T4"> The fourth type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch Ignore<T1, T2, T3, T4>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
        {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent patterns.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <typeparam name="T3"> The third type of element to filter out. </typeparam>
        /// <typeparam name="T4"> The fourth type of element to filter out. </typeparam>
        /// <typeparam name="T5"> The fifth type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch Ignore<T1, T2, T3, T4, T5>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
        {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4 || v is T5));
            return this;
        }

        /// <summary>
        /// Filters elements matching the supplied predicate out of the sequence before attempting to match any subsequent patterns.
        /// </summary>
        /// <param name="predicate"> The predicate which selects which elements to filter. </param>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch Ignore(Func<ILexical, bool> predicate)
        {
            predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Filters elements of the given type out of the sequence before attempting to match the next pattern.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch IgnoreOnce<T1>()
            where T1 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the next pattern.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch IgnoreOnce<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1 || v is T2));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the next pattern.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <typeparam name="T3"> The third type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch IgnoreOnce<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the next pattern.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <typeparam name="T3"> The third type of element to filter out. </typeparam>
        /// <typeparam name="T4"> The fourth type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch IgnoreOnce<T1, T2, T3, T4>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the next pattern.
        /// </summary>
        /// <typeparam name="T1"> The first type of element to filter out. </typeparam>
        /// <typeparam name="T2"> The second type of element to filter out. </typeparam>
        /// <typeparam name="T3"> The third type of element to filter out. </typeparam>
        /// <typeparam name="T4"> The fourth type of element to filter out. </typeparam>
        /// <typeparam name="T5"> The fifth type of element to filter out. </typeparam>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch IgnoreOnce<T1, T2, T3, T4, T5>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4 || v is T5));
            return this;
        }

        /// <summary>
        /// Filters elements matching the specified predicate out of the sequence before attempting to match the next pattern.
        /// </summary>
        /// <param name="predicate"> The predicate to apply to the sequence. </param>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch IgnoreOnce(Func<ILexical, bool> predicate)
        {
            checkOncePredicates.Add(predicate);
            return this;
        }

        #endregion Ignore Clauses

        #region Guard Clauses

        /// <summary>
        /// Predicates the next bind on the specified condition.
        /// </summary>
        /// <param name="condition"> The condition which must be met for the next binding function to be attempted. </param>
        /// <returns> The SequenceMatch instance representing the binding so far. </returns>
        public SequenceMatch When(bool condition)
        {
            guardSatisfied = condition;
            guarded = true;
            return this;
        }

        /// <summary>
        /// Predicates the next bind on the specified condition.
        /// </summary>
        /// <param name="condition"> The condition which must be met for the next binding function to be attempted. </param>
        /// <returns> The SequenceMatch instance representing the binding so far. </returns>
        public SequenceMatch When(Func<bool> condition)
        {
            guardSatisfied = condition();
            guarded = true;
            return this;
        }

        private SequenceMatch CheckGuard(Action onSuccess)
        {
            if (!Accepted && ApplicableGuardsSatisfied)
            {
                onSuccess();
                Elements = Elements.Skip(indexOfLast);
                guarded = false;
            }

            return this;
        }

        #endregion Guard Clauses

        /// <summary>
        /// Set the continuation mode of the SequenceMatch.
        /// </summary>
        /// <param name="mode"> The continuation mode to set. </param>
        /// <returns> The SentenceMatch so far. </returns>
        public SequenceMatch WithContinuationMode(ContinuationMode mode)
        {
            continuationMode = mode;
            return this;
        }

        /// <summary>
        /// Appends a log action to the <see cref="SequenceMatch"/>.
        /// </summary>
        /// <param name="log"> The log action. </param>
        /// <returns> The <see cref="SequenceMatch"/>. </returns>
        public SequenceMatch AddLogger(Action<object> log)
        {
            this.log += message => log(message);
            return this;
        }

        #region Private fields and properties

        private Action<string> log = obj => { };

        /// <summary>
        /// A value indicating whether or not the a pattern has been matched.
        /// </summary>
        private bool Accepted { get; set; }

        private IReadOnlyList<ILexical> FilterByCurrentPredicates(IReadOnlyList<ILexical> values)
        {
            var results = new List<ILexical>(values.Count);
            var tests = checkOncePredicates.Concat(predicates);
            var i = 0;
            for (; i < values.Count; ++i)
            {
                var lexical = values[i];
                if (test(lexical))
                {
                    results.Add(lexical);
                }
            }

            indexOfLast = i;

            return results;
            bool test(ILexical e) => tests.All(t => t(e));
        }

        private IReadOnlyList<ILexical> Elements
        {
            get => elements;
            set => elements = value;
        }

        private IReadOnlyList<ILexical> SequenceFilteredByCurrentPredicates => FilterByCurrentPredicates(Elements);

        /// <summary>
        /// <c> true </c> if all guards have been satisfied or there are no applicable guards; otherwise, <c> false </c>.
        /// </summary>
        private bool ApplicableGuardsSatisfied => guarded && guardSatisfied || !guarded;

        private ContinuationMode continuationMode;
        private bool guardSatisfied;
        private bool guarded;
        private readonly List<Func<ILexical, bool>> predicates = new List<Func<ILexical, bool>>();
        private readonly List<Func<ILexical, bool>> checkOncePredicates = new List<Func<ILexical, bool>>();
        private IReadOnlyList<ILexical> elements;
        private int indexOfLast;

        #endregion Private fields and properties
    }
}