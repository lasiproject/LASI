using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    static class BindingHelper
    {
        internal static bool Applicable<T1, T2, TLexical>(this Action<T1, T2> pattern, IReadOnlyList<TLexical> elements)
                 where T1 : ILexical
                 where T2 : ILexical
                 where TLexical : ILexical
        {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2;
        }
        internal static bool Applicable<T1, T2, T3, TLexical>(this Action<T1, T2, T3> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : ILexical
                      where T2 : ILexical
                      where T3 : ILexical
                      where TLexical : ILexical
        {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Action<T1, T2, T3, T4> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : ILexical
                         where T2 : ILexical
                         where T3 : ILexical
                         where T4 : ILexical
                         where TLexical : ILexical
        {
            return elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Action<T1, T2, T3, T4, T5> pattern, IReadOnlyList<TLexical> elements)
                        where T1 : ILexical
                        where T2 : ILexical
                        where T3 : ILexical
                        where T4 : ILexical
                        where T5 : ILexical
                        where TLexical : ILexical
        {
            return elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;

        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Action<T1, T2, T3, T4, T5, T6> pattern, IReadOnlyList<TLexical> elements)
                        where T1 : ILexical
                        where T2 : ILexical
                        where T3 : ILexical
                        where T4 : ILexical
                        where T5 : ILexical
                        where T6 : ILexical
                        where TLexical : ILexical
        {
            return elements.Count >= 6 &&
               elements[0] is T1 &&
               elements[1] is T2 &&
               elements[2] is T3 &&
               elements[3] is T4 &&
               elements[4] is T5 &&
               elements[5] is T6;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7> pattern, IReadOnlyList<TLexical> elements)
                       where T1 : ILexical
                       where T2 : ILexical
                       where T3 : ILexical
                       where T4 : ILexical
                       where T5 : ILexical
                       where T6 : ILexical
                       where T7 : ILexical
                       where TLexical : ILexical
        {
            return elements.Count >= 7 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : ILexical
                      where T2 : ILexical
                      where T3 : ILexical
                      where T4 : ILexical
                      where T5 : ILexical
                      where T6 : ILexical
                      where T7 : ILexical
                      where T8 : ILexical
                      where TLexical : ILexical
        {
            return elements.Count >= 8 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern, IReadOnlyList<TLexical> elements)
                     where T1 : ILexical
                     where T2 : ILexical
                     where T3 : ILexical
                     where T4 : ILexical
                     where T5 : ILexical
                     where T6 : ILexical
                     where T7 : ILexical
                     where T8 : ILexical
                     where T9 : ILexical
                     where TLexical : ILexical
        {
            return elements.Count >= 9 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern, IReadOnlyList<TLexical> elements) where T1 : ILexical
                     where T2 : ILexical
                     where T3 : ILexical
                     where T4 : ILexical
                     where T5 : ILexical
                     where T6 : ILexical
                     where T7 : ILexical
                     where T8 : ILexical
                     where T9 : ILexical
                     where T10 : ILexical
                      where TLexical : ILexical
        {
            return elements.Count >= 10 &&
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern, IReadOnlyList<TLexical> elements)
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
                     where TLexical : ILexical
        {
            return elements.Count >= 11 &&
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
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern, IReadOnlyList<TLexical> elements)
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
                   where TLexical : ILexical
        {
            return elements.Count >= 12 &&
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
        }


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern, IReadOnlyList<TLexical> elements)
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
               where TLexical : ILexical
        {
            return elements.Count >= 13 &&
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
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern, IReadOnlyList<TLexical> elements)
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
           where TLexical : ILexical
        {
            return elements.Count >= 14 &&
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
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern, IReadOnlyList<TLexical> elements)
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
        where TLexical : ILexical
        {
            return elements.Count >= 15 &&
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
        }


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern, IReadOnlyList<TLexical> elements)
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
where TLexical : ILexical
        {
            return elements.Count >= 16 &&
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
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> pattern, IReadOnlyList<TLexical> elements)
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
where TLexical : ILexical
        {
            return elements.Count >= 17 &&
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
        }



        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> pattern, IReadOnlyList<TLexical> elements)
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
where TLexical : ILexical
        {
            return elements.Count >= 18 &&
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
        }


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> pattern, IReadOnlyList<TLexical> elements)
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
where TLexical : ILexical
        {
            return elements.Count >= 19 &&
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
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> pattern, IReadOnlyList<TLexical> elements)
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
where TLexical : ILexical
        {
            return elements.Count >= 19 &&
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
              elements[18] is T20;
        }
        internal static bool Applicable<T1, T2, TLexical>(this Func<T1, Action<T2>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : ILexical
                      where T2 : ILexical
                      where TLexical : ILexical
        {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2;
        }
        internal static bool Applicable<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : ILexical
                      where T2 : ILexical
                      where T3 : ILexical
                      where TLexical : ILexical
        {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : ILexical
                         where T2 : ILexical
                         where T3 : ILexical
                         where T4 : ILexical
                         where TLexical : ILexical
        {
            return elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : ILexical
                      where T2 : ILexical
                      where T3 : ILexical
                      where T4 : ILexical
                      where T5 : ILexical
                      where TLexical : ILexical
        {
            return elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : ILexical
                   where T2 : ILexical
                   where T3 : ILexical
                   where T4 : ILexical
                   where T5 : ILexical
                   where T6 : ILexical
                   where TLexical : ILexical
        {
            return elements.Count == 6 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                  where T1 : ILexical
                  where T2 : ILexical
                  where T3 : ILexical
                  where T4 : ILexical
                  where T5 : ILexical
                  where T6 : ILexical
                  where T7 : ILexical
                  where TLexical : ILexical
        {
            return elements.Count == 7 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                where T1 : ILexical
                where T2 : ILexical
                where T3 : ILexical
                where T4 : ILexical
                where T5 : ILexical
                where T6 : ILexical
                where T7 : ILexical
                where T8 : ILexical
                where TLexical : ILexical
        {
            return elements.Count == 8 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
              where T1 : ILexical
              where T2 : ILexical
              where T3 : ILexical
              where T4 : ILexical
              where T5 : ILexical
              where T6 : ILexical
              where T7 : ILexical
              where T8 : ILexical
              where T9 : ILexical
              where TLexical : ILexical
        {
            return elements.Count == 9 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
            where T1 : ILexical
            where T2 : ILexical
            where T3 : ILexical
            where T4 : ILexical
            where T5 : ILexical
            where T6 : ILexical
            where T7 : ILexical
            where T8 : ILexical
            where T9 : ILexical
            where TLexical : ILexical
        {
            return elements.Count == 10 &&
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
        internal static void Apply<T1, T2, TLexical>(
            this Func<T1, Action<T2>> pattern, IReadOnlyList<TLexical> elements)
                                  where T1 : class, ILexical
                                  where T2 : class, ILexical
                                  where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(elements[1] as T2);
        }
        internal static void Apply<T1, T2, T3, TLexical>(
            this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<TLexical> elements)
                                  where T1 : class, ILexical
                                  where T2 : class, ILexical
                                  where T3 : class, ILexical
                                  where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(elements[1] as T2)(elements[2] as T3);
        }
        internal static void Apply<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern,
            IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(
                    elements[1] as T2)(
                    elements[2] as T3)(
                    elements[3] as T4);
        }
        internal static void Apply<T1, T2, T3, T4, T5, TLexical>(
            this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern,
            IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(elements[1] as T2)(elements[2] as T3)(elements[3] as T4)(elements[4] as T5);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(
                    elements[1] as T2)(
                    elements[2] as T3)(
                    elements[3] as T4)(
                    elements[4] as T5)(
                    elements[5] as T6);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
                  where TLexical : class, ILexical
        {
            pattern(
                elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
                where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical
              where TLexical : class, ILexical
        {
            pattern(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8)(
                elements[8] as T9);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
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
            pattern(elements[0] as T1)
                (elements[1] as T2)
                (elements[2] as T3)
                (elements[3] as T4)
                (elements[4] as T5)
                (elements[5] as T6)
                (elements[6] as T7)
                (elements[7] as T8)
                (elements[8] as T9)
                (elements[9] as T10);
        }
        internal static bool ApplyIfApplicable<T1, T2>(this Func<T1, Action<T2>> pattern, IReadOnlyList<ILexical> elements)
                                where T1 : class, ILexical
                                where T2 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<ILexical> elements)
                                where T1 : class, ILexical
                                where T2 : class, ILexical
                                where T3 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<ILexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<ILexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<ILexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<ILexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<ILexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<ILexical> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<ILexical> elements)
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
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<Phrase> elements)
                               where T1 : class, ILexical
                               where T2 : class, ILexical
                               where T3 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<Phrase> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<Phrase> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<Phrase> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<Phrase> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<Phrase> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<Phrase> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical
        {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<Phrase> elements)
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
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }

    }
}
