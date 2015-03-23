using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
// TODO: SequenceMatch like class with result returning case expressions. Probably not add them to the current class. API is large as it is.
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>Represents a binding expression applied to a sequence if lexical constructs.</summary>
    public partial class SequenceMatch
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2);
                    Sequence = Sequence.Skip(2);
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
        public SequenceMatch Case<T1, T2, T3>(Action<T1, T2, T3> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(SequenceFilteredByIgnoreOncePredicates));


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
        where T3 : class, ILexical where T4 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(SequenceFilteredByIgnoreOncePredicates));

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
        where T5 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(SequenceFilteredByIgnoreOncePredicates));

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
        where T6 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(SequenceFilteredByIgnoreOncePredicates));

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
        where T7 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(SequenceFilteredByIgnoreOncePredicates));

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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        where T8 : class, ILexical => CheckGuard(() => Accepted = pattern.Curry().ApplyIfApplicable(SequenceFilteredByIgnoreOncePredicates));

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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9);
                    Sequence = Sequence.Skip(9);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        where T10 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10);
                Sequence = Sequence.Skip(10);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11);
                    Sequence = Sequence.Skip(11);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        where T12 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                        Sequence[10] as T11, Sequence[11] as T12);
                Sequence = Sequence.Skip(12);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        where T13 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                        Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13);
                Sequence = Sequence.Skip(13);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14);
                    Sequence = Sequence.Skip(14);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1,
                    Sequence[1] as T2,
                    Sequence[2] as T3,
                    Sequence[3] as T4,
                    Sequence[4] as T5,
                    Sequence[5] as T6,
                    Sequence[6] as T7,
                    Sequence[7] as T8,
                    Sequence[8] as T9,
                    Sequence[9] as T10,
                    Sequence[10] as T11,
                    Sequence[11] as T12,
                    Sequence[12] as T13,
                    Sequence[13] as T14,
                    Sequence[14] as T15);
                    Sequence = Sequence.Skip(15);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15, Sequence[15] as T16);
                    Sequence = Sequence.Skip(16);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                            Sequence[15] as T16, Sequence[16] as T17);
                    Sequence = Sequence.Skip(17);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                    Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                    if (Accepted)
                    {
                        pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                                Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                                Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                                Sequence[15] as T16, Sequence[16] as T17, Sequence[17] as T18);
                        Sequence = Sequence.Skip(18);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                            Sequence[15] as T16, Sequence[16] as T17, Sequence[17] as T18, Sequence[18] as T19);
                    Sequence = Sequence.Skip(19);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    {
                        pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                                Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                                Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                                Sequence[15] as T16, Sequence[16] as T17, Sequence[17] as T18, Sequence[18] as T19, Sequence[19] as T20);
                        Sequence = Sequence.Skip(20);
                    }
                }
            });
        }

        #endregion Case Clauses

        #region Case With Result Clauses

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, TResult>(Func<T1, T2, TResult> pattern) where T1 : class, ILexical where T2 : class, ILexical
        {
            return CheckGuard(() =>
            {
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2);
                    Sequence = Sequence.Skip(2);
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
        public SequenceMatch Case<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3);
                Sequence = Sequence.Skip(3);
            }
        });

        /// <summary>
        /// Applies the specified binding function to the sequence when its pattern is matched.
        /// </summary>
        /// <typeparam name="T1">The type of the first element in the pattern</typeparam>
        /// <typeparam name="T2">The type of the second element in the pattern</typeparam>
        /// <typeparam name="T3">The type of the third element in the pattern</typeparam>
        /// <typeparam name="T4">The type of the fourth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> pattern)
        where T1 : class, ILexical where T2 : class, ILexical
        where T3 : class, ILexical where T4 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4);
                Sequence = Sequence.Skip(4);
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
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5);
                Sequence = Sequence.Skip(5);
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
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6);
                Sequence = Sequence.Skip(6);
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
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7);
                Sequence = Sequence.Skip(7);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8);
                Sequence = Sequence.Skip(8);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9);
                    Sequence = Sequence.Skip(9);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10);
                Sequence = Sequence.Skip(10);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11);
                    Sequence = Sequence.Skip(11);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> pattern)
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
        where T12 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                        Sequence[10] as T11, Sequence[11] as T12);
                Sequence = Sequence.Skip(12);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> pattern)
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
        where T13 : class, ILexical => CheckGuard(() =>
        {
            Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
            if (Accepted)
            {
                pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                        Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                        Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13);
                Sequence = Sequence.Skip(13);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14);
                    Sequence = Sequence.Skip(14);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
        /// <typeparam name="T9">The type of the ninth element in the pattern</typeparam>
        /// <typeparam name="T10">The type of the tenth element in the pattern</typeparam>
        /// <typeparam name="T11">The type of the eleventh element in the pattern</typeparam>
        /// <typeparam name="T12">The type of the twelfth element in the pattern</typeparam>
        /// <typeparam name="T13">The type of the thirteenth element in the pattern</typeparam>
        /// <typeparam name="T14">The type of the fourteenth element in the pattern</typeparam>
        /// <typeparam name="T15">The type of the fifteenth element in the pattern</typeparam>
        /// <param name="pattern">The binding pattern to apply.</param>
        /// <returns>The SequenceMatch instance representing the binding so far.</returns>
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1,
                    Sequence[1] as T2,
                    Sequence[2] as T3,
                    Sequence[3] as T4,
                    Sequence[4] as T5,
                    Sequence[5] as T6,
                    Sequence[6] as T7,
                    Sequence[7] as T8,
                    Sequence[8] as T9,
                    Sequence[9] as T10,
                    Sequence[10] as T11,
                    Sequence[11] as T12,
                    Sequence[12] as T13,
                    Sequence[13] as T14,
                    Sequence[14] as T15);
                    Sequence = Sequence.Skip(15);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15, Sequence[15] as T16);
                    Sequence = Sequence.Skip(16);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                            Sequence[15] as T16, Sequence[16] as T17);
                    Sequence = Sequence.Skip(17);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult> pattern)
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
                    Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                    if (Accepted)
                    {
                        pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                                Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                                Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                                Sequence[15] as T16, Sequence[16] as T17, Sequence[17] as T18);
                        Sequence = Sequence.Skip(18);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                            Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                            Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                            Sequence[15] as T16, Sequence[16] as T17, Sequence[17] as T18, Sequence[18] as T19);
                    Sequence = Sequence.Skip(19);
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
        /// <typeparam name="T8">The type of the eighth element in the pattern</typeparam>
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
        public SequenceMatch Case<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult> pattern)
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
                Accepted = pattern.Applicable(SequenceFilteredByIgnoreOncePredicates);
                if (Accepted)
                {
                    {
                        pattern(Sequence[0] as T1, Sequence[1] as T2, Sequence[2] as T3, Sequence[3] as T4, Sequence[4] as T5,
                                Sequence[5] as T6, Sequence[6] as T7, Sequence[7] as T8, Sequence[8] as T9, Sequence[9] as T10,
                                Sequence[10] as T11, Sequence[11] as T12, Sequence[12] as T13, Sequence[13] as T14, Sequence[14] as T15,
                                Sequence[15] as T16, Sequence[16] as T17, Sequence[17] as T18, Sequence[18] as T19, Sequence[19] as T20);
                        Sequence = Sequence.Skip(20);
                    }
                }
            });
        }

        #endregion Case Clauses
    }
}