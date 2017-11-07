using System;
using System.Collections.Generic;
using LASI.Utilities;

namespace LASI.Core.Analysis.Binding.Experimental.SequentialPatterns
{
    static class FuncBindingHelper
    {
        public static bool Applicable<T1, T2, TResult, TLexical>(
            this Func<T1, T2, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where TLexical : ILexical => elements.Count >= 2 &&
                elements[0] is T1 &&
                elements[1] is T2;

        public static bool Applicable<T1, T2, T3, TResult, TLexical>(
            this Func<T1, T2, T3, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where TLexical : ILexical => elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;

        public static bool Applicable<T1, T2, T3, T4, TResult, TLexical>(
            this Func<T1, T2, T3, T4, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where TLexical : ILexical => elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        public static bool Applicable<T1, T2, T3, T4, T5, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where TLexical : ILexical => elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, TResult, TLexical>(
           this Func<T1, T2, T3, T4, T5, T6, TResult> pattern,
           IReadOnlyList<TLexical> elements
       )
           where T1 : ILexical
           where T2 : ILexical
           where T3 : ILexical
           where T4 : ILexical
           where T5 : ILexical
           where T6 : ILexical
           where TLexical : ILexical => elements.Count >= 6 &&
               elements[0] is T1 &&
               elements[1] is T2 &&
               elements[2] is T3 &&
               elements[3] is T4 &&
               elements[4] is T5 &&
               elements[5] is T6;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where TLexical : ILexical => elements.Count >= 7 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where TLexical : ILexical => elements.Count >= 8 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where TLexical : ILexical => elements.Count >= 9 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> pattern,
            IReadOnlyList<TLexical> elements
        ) where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where TLexical : ILexical => elements.Count >= 10 &&
                  elements[0] is T1 &&
                  elements[1] is T2 &&
                  elements[2] is T3 &&
                  elements[3] is T4 &&
                  elements[4] is T5 &&
                  elements[5] is T6 &&
                  elements[6] is T7 &&
                  elements[7] is T8 &&
                  elements[8] is T9 &&
                  elements[9] is T10;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where TLexical : ILexical => elements.Count >= 11 &&
                  elements[0] is T1 &&
                  elements[1] is T2 &&
                  elements[2] is T3 &&
                  elements[3] is T4 &&
                  elements[4] is T5 &&
                  elements[5] is T6 &&
                  elements[6] is T7 &&
                  elements[7] is T8 &&
                  elements[8] is T9 &&
                  elements[9] is T10 &&
                  elements[10] is T11;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where TLexical : ILexical => elements.Count >= 12 &&
                  elements[0] is T1 &&
                  elements[1] is T2 &&
                  elements[2] is T3 &&
                  elements[3] is T4 &&
                  elements[4] is T5 &&
                  elements[5] is T6 &&
                  elements[6] is T7 &&
                  elements[7] is T8 &&
                  elements[8] is T9 &&
                  elements[9] is T10 &&
                  elements[10] is T11 &&
                  elements[11] is T12;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where TLexical : ILexical => elements.Count >= 13 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9 &&
                elements[9] is T10 &&
                elements[10] is T11 &&
                elements[11] is T12 &&
                elements[12] is T13;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where TLexical : ILexical => elements.Count >= 14 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9 &&
                elements[9] is T10 &&
                elements[10] is T11 &&
                elements[11] is T12 &&
                elements[12] is T13 &&
                elements[13] is T14;


        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where T15 : ILexical
            where TLexical : ILexical => elements.Count >= 15 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15;
        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where T15 : ILexical
            where T16 : ILexical
            where TLexical : ILexical => elements.Count >= 16 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16;


        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where T15 : ILexical
            where T16 : ILexical
            where T17 : ILexical
    where TLexical : ILexical => elements.Count >= 17 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16 &&
              elements[16] is T17;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where T15 : ILexical
            where T16 : ILexical
            where T17 : ILexical
            where T18 : ILexical
    where TLexical : ILexical => elements.Count >= 18 &&
      elements[0] is T1 &&
      elements[1] is T2 &&
      elements[2] is T3 &&
      elements[3] is T4 &&
      elements[4] is T5 &&
      elements[5] is T6 &&
      elements[6] is T7 &&
      elements[7] is T8 &&
      elements[8] is T9 &&
      elements[9] is T10 &&
      elements[10] is T11 &&
      elements[11] is T12 &&
      elements[12] is T13 &&
      elements[13] is T14 &&
      elements[14] is T15 &&
      elements[15] is T16 &&
      elements[16] is T17 &&
      elements[17] is T18;



        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where T15 : ILexical
            where T16 : ILexical
            where T17 : ILexical
            where T18 : ILexical
            where T19 : ILexical
            where TLexical : ILexical =>
    elements.Count >= 19 &&
    elements[0] is T1 &&
    elements[1] is T2 &&
    elements[2] is T3 &&
    elements[3] is T4 &&
    elements[4] is T5 &&
    elements[5] is T6 &&
    elements[6] is T7 &&
    elements[7] is T8 &&
    elements[8] is T9 &&
    elements[9] is T10 &&
    elements[10] is T11 &&
    elements[11] is T12 &&
    elements[12] is T13 &&
    elements[13] is T14 &&
    elements[14] is T15 &&
    elements[15] is T16 &&
    elements[16] is T17 &&
    elements[17] is T18 &&
    elements[18] is T19;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult, TLexical>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where T10 : ILexical
            where T11 : ILexical
            where T12 : ILexical
            where T13 : ILexical
            where T14 : ILexical
            where T15 : ILexical
            where T16 : ILexical
            where T17 : ILexical
            where T18 : ILexical
            where T19 : ILexical
            where T20 : ILexical
            where TLexical : ILexical =>
    elements.Count >= 20 &&
    elements[0] is T1 &&
    elements[1] is T2 &&
    elements[2] is T3 &&
    elements[3] is T4 &&
    elements[4] is T5 &&
    elements[5] is T6 &&
    elements[6] is T7 &&
    elements[7] is T8 &&
    elements[8] is T9 &&
    elements[9] is T10 &&
    elements[10] is T11 &&
    elements[11] is T12 &&
    elements[12] is T13 &&
    elements[13] is T14 &&
    elements[14] is T15 &&
    elements[15] is T16 &&
    elements[16] is T17 &&
    elements[17] is T18 &&
    elements[18] is T19 &&
    elements[19] is T20;

        public static bool Applicable<T1, T2, TResult, TLexical>(
            this Func<T1, Action<T2>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where TLexical : ILexical => elements.Count >= 2 &&
                elements[0] is T1 &&
                elements[1] is T2;

        public static bool Applicable<T1, T2, T3, TResult, TLexical>(
            this Func<T1, Func<T2, Action<T3>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where TLexical : ILexical =>
         elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;

        public static bool Applicable<T1, T2, T3, T4, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Action<T4>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where TLexical : ILexical => elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        public static bool Applicable<T1, T2, T3, T4, T5, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where TLexical : ILexical => elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;
        public static bool Applicable<T1, T2, T3, T4, T5, T6, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where TLexical : ILexical => elements.Count == 6 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where TLexical : ILexical => elements.Count == 7 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where TLexical : ILexical => elements.Count == 8 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where TLexical : ILexical => elements.Count == 9 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9;

        public static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>, TResult> pattern,
            IReadOnlyList<TLexical> elements
        )
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where TLexical : ILexical => elements.Count == 10 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9 &&
                elements[9] is T10;

    }
}
