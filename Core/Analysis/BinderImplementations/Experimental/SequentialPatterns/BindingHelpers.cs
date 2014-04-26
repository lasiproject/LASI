using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using LASI.Core.DocumentStructures;



namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{


    public static class Matcher
    {
        internal static SentenceMatch Match(this Sentence sentence) {
            return new SentenceMatch(sentence);
        }

        internal static SentenceMatch Match(this IEnumerable<ILexical> sequencialElements) {

            return new SentenceMatch(sequencialElements);
        }

    }

    public partial class SentenceMatch
    {
        private bool predicateSucceded;
        private bool guarded;
        List<Func<ILexical, bool>> predicates = new List<Func<ILexical, bool>>();
        List<Func<ILexical, bool>> checkOncePredicates = new List<Func<ILexical, bool>>();

        private IEnumerable<ILexical> value;

        public SentenceMatch(IEnumerable<ILexical> sequencialElements) {
            value = sequencialElements;
        }
        public SentenceMatch(Sentence setence) {
            value = setence.Phrases;

        }
        private IEnumerable<ILexical> test(IEnumerable<ILexical> val) {


            var result = from v in val.OfClause() where checkOncePredicates.All(f => f(v)) && predicates.All(f => f(v)) select v;
            checkOncePredicates.Clear();
            return result;


        }

        private List<ILexical> values {
            get { return test(value).ToList(); }
            set { values = value; }
        }
        protected bool Accepted { get; set; }
        internal ContinuationMode ContinuationMode {
            get;
            private set;
        }

        public SentenceMatch TryPath<T1, T2, T3>(Action<T1, T2, T3> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3);
                values = values.Skip(3);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4);
                values = values.Skip(4);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5);
                values = values.Skip(5);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6);
                values = values.Skip(6);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7);
                values = values.Skip(7);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8);
                values = values.Skip(8);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical {

            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9);
                values = values.Skip(9);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10);
                values = values.Skip(10);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11);
                values = values.Skip(11);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12);
                values = values.Skip(12);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical
        where T13 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13);
                values = values.Skip(13);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical
        where T13 : class, ILexical
        where T14 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14);
                values = values.Skip(14);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical
        where T13 : class, ILexical
        where T14 : class, ILexical
        where T15 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15);
                values = values.Skip(15);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical
        where T13 : class, ILexical
        where T14 : class, ILexical
        where T15 : class, ILexical
        where T16 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15, values[15] as T16);
                values = values.Skip(16);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical
        where T13 : class, ILexical
        where T14 : class, ILexical
        where T15 : class, ILexical
        where T16 : class, ILexical
        where T17 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15,
                            values[15] as T16, values[16] as T17);
                values = values.Skip(17);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical
            where T11 : class, ILexical
            where T12 : class, ILexical
            where T13 : class, ILexical
            where T14 : class, ILexical
            where T15 : class, ILexical
            where T16 : class, ILexical
            where T17 : class, ILexical
            where T18 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15,
                            values[15] as T16, values[16] as T17, values[17] as T18);
                values = values.Skip(18);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical
            where T11 : class, ILexical
            where T12 : class, ILexical
            where T13 : class, ILexical
            where T14 : class, ILexical
            where T15 : class, ILexical
            where T16 : class, ILexical
            where T17 : class, ILexical
            where T18 : class, ILexical
            where T19 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15,
                            values[15] as T16, values[16] as T17, values[17] as T18, values[18] as T19);
                values = values.Skip(19);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical
            where T11 : class, ILexical
            where T12 : class, ILexical
            where T13 : class, ILexical
            where T14 : class, ILexical
            where T15 : class, ILexical
            where T16 : class, ILexical
            where T17 : class, ILexical
            where T18 : class, ILexical
            where T19 : class, ILexical
            where T20 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted) {
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15,
                            values[15] as T16, values[16] as T17, values[17] as T18, values[18] as T19, values[19] as T20);
                    values = values.Skip(20);
                }
            });
        }




        public SentenceMatch When(bool condition) {
            predicateSucceded = condition;
            return this;
        }
        public SentenceMatch When(Func<bool> condition) {
            predicateSucceded = condition();
            guarded = true;
            return this;
        }
        private SentenceMatch CheckGuard(Action onSuccess) {
            if (!Accepted && guarded && predicateSucceded) { onSuccess(); guarded = false; }
            return this;
        }
        /// <summary>
        /// Filters elements of the given type out of the sequence before attempting to match any subsequent paths.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SentenceMatch FilterContinuously<T1>()
            where T1 : class, ILexical {
            predicates.Add(v => !(v is T1));
            return this;
        }
        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent paths.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SentenceMatch FilterContinuously<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2));
            return this;
        }
        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent paths.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SentenceMatch FilterContinuously<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }
        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent paths.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <typeparam name="T4">The fourth type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SentenceMatch FilterContinuously<T1, T2, T3, T4>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4));
            return this;
        }
        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any subsequent paths.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <typeparam name="T4">The fourth type of element to filter out.</typeparam>
        /// <typeparam name="T5">The fifth type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SentenceMatch FilterContinuously<T1, T2, T3, T4, T5>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4 || v is T5));
            return this;
        }
        /// <summary>
        /// Filters elements matching the supplied predicate out of the sequence before attempting to match any subsequent paths.
        /// </summary>
        /// <param name="predicate">The predicate which selects which elements to filter.</param>
        /// <returns>The SentenceMatch so far.</returns>
        public SentenceMatch FilterContinuously(Func<ILexical, bool> predicate) {
            predicates.Add(predicate);
            return this;
        }
        public SentenceMatch FilterOnce<T1>()
        where T1 : class, ILexical {
            checkOncePredicates.Add(v => !(v is T1));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical {
            checkOncePredicates.Add(v => !(v is T1 || v is T2));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2, T3, T4>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2, T3, T4, T5>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4 || v is T5));
            return this;
        }
        public SentenceMatch FilterOnce(Func<ILexical, bool> predicate) {
            checkOncePredicates.Add(predicate);
            return this;
        }
        public SentenceMatch WithContinuationMode(ContinuationMode mode) {
            this.ContinuationMode = mode;
            return this;
        }
    }
}

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    static class ListExtensions
    {
        public static List<T> Take<T>(this List<T> source, int count) {
            return source.GetRange(0, count);
        }
        public static List<T> Skip<T>(this List<T> source, int count) {
            try {
                return source.GetRange(count, source.Count);
            }
            catch (ArgumentException) {
                return new List<T>();
            }
        }

    }

}

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    public enum ContinuationMode
    {
        Recursive,
        TraverseOnce,
    }
}