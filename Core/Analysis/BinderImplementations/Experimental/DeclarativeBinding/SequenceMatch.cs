using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.Linq.List;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>Represents a binding expression applied to a sequence if lexical constructs.</summary>
    public class SequenceMatch
    {

        #region Case Clauses

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2>(Action<T1, T2> pattern) where T1 : class, ILexical where T2 : class, ILexical
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2);
                    Values = Values.Skip(3);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3>(Action<T1, T2, T3> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical
        {
            return CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(ValuesFilteredByIgnoreOncePredicates));
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical =>
            CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(ValuesFilteredByIgnoreOncePredicates));

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(ValuesFilteredByIgnoreOncePredicates));

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(ValuesFilteredByIgnoreOncePredicates));

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(ValuesFilteredByIgnoreOncePredicates));

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(ValuesFilteredByIgnoreOncePredicates));

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical =>
            CheckGuard(() =>
             {
                 Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                 if (Accepted)
                 {
                     pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                             Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9);
                     Values = Values.Skip(9);
                 }
             });

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10);
                    Values = Values.Skip(10);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11);
                    Values = Values.Skip(11);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11, Values[11] as T12);
                    Values = Values.Skip(12);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11, Values[11] as T12, Values[12] as T13);
                    Values = Values.Skip(13);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14);
                    Values = Values.Skip(14);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1,
                    Values[1] as T2,
                    Values[2] as T3,
                    Values[3] as T4,
                    Values[4] as T5,
                    Values[5] as T6,
                    Values[6] as T7,
                    Values[7] as T8,
                    Values[8] as T9,
                    Values[9] as T10,
                    Values[10] as T11,
                    Values[11] as T12,
                    Values[12] as T13,
                    Values[13] as T14,
                    Values[14] as T15);
                    Values = Values.Skip(15);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <typeparam name="T16">The type of the sixteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15, Values[15] as T16);
                    Values = Values.Skip(16);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <typeparam name="T16">The type of the sixteenth element in the pattern</typeparam>
        /// <typeparam name="T17">The type of the seventeenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15,
                            Values[15] as T16, Values[16] as T17);
                    Values = Values.Skip(17);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <typeparam name="T16">The type of the sixteenth element in the pattern</typeparam>
        /// <typeparam name="T17">The type of the seventeenth element in the pattern</typeparam>
        /// <typeparam name="T18">The type of the eighteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> pattern)
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
            where T18 : class, ILexical =>
                CheckGuard(() =>
                {
                    Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                    if (Accepted)
                    {
                        pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                                Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                                Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15,
                                Values[15] as T16, Values[16] as T17, Values[17] as T18);
                        Values = Values.Skip(18);
                    }
                });


        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <typeparam name="T16">The type of the sixteenth element in the pattern</typeparam>
        /// <typeparam name="T17">The type of the seventeenth element in the pattern</typeparam>
        /// <typeparam name="T18">The type of the eighteenth element in the pattern</typeparam>
        /// <typeparam name="T19">The type of the nineteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> pattern)
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
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                            Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                            Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15,
                            Values[15] as T16, Values[16] as T17, Values[17] as T18, Values[18] as T19);
                    Values = Values.Skip(19);
                }
            });
        }

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <typeparam name="T5">The type of the fifth element in the pattern</typeparam>
        /// <typeparam name="T6">The type of the sixth element in the pattern</typeparam>
        /// <typeparam name="T7">The type of the seventh element in the pattern</typeparam>
        /// <typeparam name="T8">The type of the eigth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <typeparam name="T16">The type of the sixteenth element in the pattern</typeparam>
        /// <typeparam name="T17">The type of the seventeenth element in the pattern</typeparam>
        /// <typeparam name="T18">The type of the eighteenth element in the pattern</typeparam>
        /// <typeparam name="T19">The type of the nineteenth element in the pattern</typeparam>
        /// <typeparam name="T20">The type of the twentieth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> pattern)
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
            where T20 : class, ILexical
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(ValuesFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    {
                        pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5,
                                Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10,
                                Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15,
                                Values[15] as T16, Values[16] as T17, Values[17] as T18, Values[18] as T19, Values[19] as T20);
                        Values = Values.Skip(20);
                    }
                }
            });
        }

        #endregion Case Clauses

        #region Ignore Clauses

        /// <summary>
        /// Filters elements of the given type out of the sequence before attempting to match any
        /// subsequent patterns.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch Ignore<T1>()
            where T1 : class, ILexical
        {
            predicates.Add(v => !(v is T1));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any
        /// subsequent patterns.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch Ignore<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical
        {
            predicates.Add(v => !(v is T1 || v is T2));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any
        /// subsequent patterns.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch Ignore<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
        {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match any
        /// subsequent patterns.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <typeparam name="T4">The fourth type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
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
        /// Filters elements of the given types out of the sequence before attempting to match any
        /// subsequent patterns.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <typeparam name="T4">The fourth type of element to filter out.</typeparam>
        /// <typeparam name="T5">The fifth type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
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
        /// Filters elements matching the supplied predicate out of the sequence before attempting
        /// to match any subsequent patterns.
        /// </summary>
        /// <param name="predicate">The predicate which selects which elements to filter.</param>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch Ignore(Func<ILexical, bool> predicate)
        {
            predicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Filters elements of the given type out of the sequence before attempting to match the
        /// next pattern.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch IgnoreOnce<T1>()
        where T1 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the
        /// next pattern.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch IgnoreOnce<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1 || v is T2));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the
        /// next pattern.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch IgnoreOnce<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
        {
            checkOncePredicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }

        /// <summary>
        /// Filters elements of the given types out of the sequence before attempting to match the
        /// next pattern.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <typeparam name="T4">The fourth type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
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
        /// Filters elements of the given types out of the sequence before attempting to match the
        /// next pattern.
        /// </summary>
        /// <typeparam name="T1">The first type of element to filter out.</typeparam>
        /// <typeparam name="T2">The second type of element to filter out.</typeparam>
        /// <typeparam name="T3">The third type of element to filter out.</typeparam>
        /// <typeparam name="T4">The fourth type of element to filter out.</typeparam>
        /// <typeparam name="T5">The fifth type of element to filter out.</typeparam>
        /// <returns>The SentenceMatch so far.</returns>
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
        /// Filters elements matching the specified predicate out of the sequence before attempting
        /// to match the next pattern.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the sequence.</param>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch IgnoreOnce(Func<ILexical, bool> predicate)
        {
            checkOncePredicates.Add(predicate);
            return this;
        }

        #endregion Ignore Clauses

        #region Guard Clauses

        /// <summary>Predicates the next bind on the specified condition.</summary>
        /// <param name="condition">
        /// The condition which must be met for the next binding function to be attempted.
        /// </param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Guard(bool condition)
        {
            guardSatisfied = condition;
            guarded = true;
            return this;
        }

        /// <summary>Predicates the next bind on the specified condition.</summary>
        /// <param name="condition">
        /// The condition which must be met for the next binding function to be attempted.
        /// </param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Guard(Func<bool> condition)
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
                guarded = false;
            }
            return this;
        }

        #endregion Guard Clauses

        /// <summary>Set the continuation mode of the SequenceMatch.</summary>
        /// <param name="mode">The continuation mode to set.</param>
        /// <returns>The SentenceMatch so far.</returns>
        public SequenceMatch WithContinuationMode(ContinuationMode mode)
        {
            continuationMode = mode;
            return this;
        }

        public SequenceMatch AddLoggingBehavior(Action<object> loggingFunction)
        {
            Log += loggingFunction;
            return this;
        }

        internal SequenceMatch(IEnumerable<ILexical> sequencialElements)
        {
            values = sequencialElements.ToList();
        }

        internal SequenceMatch(Sentence setence)
        {
            values = setence.Phrases.Select(p => p as ILexical).ToList();
        }

        #region Private fields and properties

        public Action<string> Log { get; private set; } = delegate { };

        /// <summary>
        /// Gets or sets the value indicating whether or not the a pattern has been matched.
        /// </summary>
        /// <returns></returns>
        protected bool Accepted { get; set; }

        private IReadOnlyList<ILexical> FilterByIgnoreOncePredicates(List<ILexical> values)
        {
            var result = from value in values/*.OfClause()*/
                         where checkOncePredicates.All(f => f(value)) && predicates.All(f => f(value))
                         select value;
            checkOncePredicates.Clear();
            return result;
        }

        private List<ILexical> Values
        {
            get { return values; }
            set { this.values = value; }
        }

        private IReadOnlyList<ILexical> ValuesFilteredByIgnoreOncePredicates => FilterByIgnoreOncePredicates(Values);

        /// <summary>
        /// <c>true</c> if all guards have been satisfied or there are no applicable guards;
        /// otherwise, <c>false</c>.
        /// </summary>
        private bool ApplicableGuardsSatisfied => guarded && guardSatisfied || !guarded;

        private ContinuationMode continuationMode;
        private bool guardSatisfied;
        private bool guarded;
        private List<Func<ILexical, bool>> predicates = new List<Func<ILexical, bool>>();
        private List<Func<ILexical, bool>> checkOncePredicates = new List<Func<ILexical, bool>>();
        private List<ILexical> values;

        #endregion Private fields and properties
    }

    /// <summary>Determines how a match should be applied to a sentence or sentence fragment.</summary>
    public enum ContinuationMode
    {
        /// <summary>After a successful match, no continuation will be performed.</summary>
        None = 0,

        /// <summary>
        /// After a successful match, the same patterns will be applied to the remainder of the
        /// sentence or sentence fragment.
        /// </summary>
        Recursive,
    }
}