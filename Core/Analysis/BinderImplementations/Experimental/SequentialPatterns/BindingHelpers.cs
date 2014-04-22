using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using LASI.Core.DocumentStructures;



namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{


    static class BindingHelper
    {
        internal static bool Applicable<T1, T2, T3, TLexical>(this Action<T1, T2, T3> pattern,
            IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Action<T1, T2, T3, T4> pattern,
            IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            return elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Action<T1, T2, T3, T4, T5> pattern,
            IReadOnlyList<TLexical> elements)
                        where T1 : class, ILexical
                        where T2 : class, ILexical
                        where T3 : class, ILexical
                        where T4 : class, ILexical
                        where T5 : class, ILexical
                        where TLexical : class, ILexical {
            return elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;

        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Action<T1, T2, T3, T4, T5, T6> pattern,
            IReadOnlyList<TLexical> elements)
                        where T1 : class, ILexical
                        where T2 : class, ILexical
                        where T3 : class, ILexical
                        where T4 : class, ILexical
                        where T5 : class, ILexical
                        where T6 : class, ILexical
                        where TLexical : class, ILexical {
            return elements.Count >= 6 &&
               elements[0] is T1 &&
               elements[1] is T2 &&
               elements[2] is T3 &&
               elements[3] is T4 &&
               elements[4] is T5 &&
               elements[5] is T6;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7> pattern,
            IReadOnlyList<TLexical> elements)
                       where T1 : class, ILexical
                       where T2 : class, ILexical
                       where T3 : class, ILexical
                       where T4 : class, ILexical
                       where T5 : class, ILexical
                       where T6 : class, ILexical
                       where T7 : class, ILexical
                       where TLexical : class, ILexical {
            return elements.Count >= 7 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> pattern,
            IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where T6 : class, ILexical
                      where T7 : class, ILexical
                      where T8 : class, ILexical
                      where TLexical : class, ILexical {
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern,
            IReadOnlyList<TLexical> elements)
                     where T1 : class, ILexical
                     where T2 : class, ILexical
                     where T3 : class, ILexical
                     where T4 : class, ILexical
                     where T5 : class, ILexical
                     where T6 : class, ILexical
                     where T7 : class, ILexical
                     where T8 : class, ILexical
                     where T9 : class, ILexical
                     where TLexical : class, ILexical {
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern,
            IReadOnlyList<TLexical> elements) where T1 : class, ILexical
                     where T2 : class, ILexical
                     where T3 : class, ILexical
                     where T4 : class, ILexical
                     where T5 : class, ILexical
                     where T6 : class, ILexical
                     where T7 : class, ILexical
                     where T8 : class, ILexical
                     where T9 : class, ILexical
                     where T10 : class, ILexical
                      where TLexical : class, ILexical {
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern,
            IReadOnlyList<TLexical> elements)
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
                     where TLexical : class, ILexical {
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

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern,
            IReadOnlyList<TLexical> elements)
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
                   where TLexical : class, ILexical {
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


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern,
            IReadOnlyList<TLexical> elements)
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
               where TLexical : class, ILexical {
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern,
            IReadOnlyList<TLexical> elements)
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
           where TLexical : class, ILexical {
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

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern,
            IReadOnlyList<TLexical> elements)
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
        where TLexical : class, ILexical {
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


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern,
            IReadOnlyList<TLexical> elements)
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
where TLexical : class, ILexical {
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

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> pattern,
            IReadOnlyList<TLexical> elements)
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
where TLexical : class, ILexical {
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



        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> pattern,
            IReadOnlyList<TLexical> elements)
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
where TLexical : class, ILexical {
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
where TLexical : class, ILexical {
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
where TLexical : class, ILexical {
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

        internal static bool Applicable<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            return elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical {
            return elements.Count == 6 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
                  where TLexical : class, ILexical {
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
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
                where TLexical : class, ILexical {
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
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical
              where TLexical : class, ILexical {
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
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where TLexical : class, ILexical {
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



        internal static void Apply<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<TLexical> elements)
                                  where T1 : class, ILexical
                                  where T2 : class, ILexical
                                  where T3 : class, ILexical
                                  where TLexical : class, ILexical {
            pattern(elements[0] as T1)(elements[1] as T2)(elements[2] as T3);
        }
        internal static void Apply<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            pattern(
                    elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4);
        }
        internal static void Apply<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical {
            pattern(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical {
            pattern(
               elements[0] as T1)(
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
                  where TLexical : class, ILexical {
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
                where TLexical : class, ILexical {
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
              where TLexical : class, ILexical {
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
            where T10 : class, ILexical {
            pattern(
                   elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8)(
                elements[8] as T9)(
                elements[9] as T10);
        }

        internal static bool ApplyIfApplicable<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<ILexical> elements)
                                where T1 : class, ILexical
                                where T2 : class, ILexical
                                where T3 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<ILexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical {
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
                      where T5 : class, ILexical {
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
                   where T6 : class, ILexical {
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
                  where T7 : class, ILexical {
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
                where T8 : class, ILexical {
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
              where T9 : class, ILexical {
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
            where T10 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<Phrase> elements)
                               where T1 : class, ILexical
                               where T2 : class, ILexical
                               where T3 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<Phrase> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical {
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
                      where T5 : class, ILexical {
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
                   where T6 : class, ILexical {
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
                  where T7 : class, ILexical {
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
                where T8 : class, ILexical {
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
              where T9 : class, ILexical {
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
            where T10 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }

    }
    internal static class Matcher
    {
        internal static SentenceMatch Match(this Sentence sentence) {
            return new SentenceMatch(sentence);
        }
    }
    public partial class SentenceMatch(private Sentence value)
    {
        private bool predicateSucceded;
        private bool guarded;
        List<Func<ILexical, bool>> predicates = new List<Func<ILexical, bool>>();
        List<Func<ILexical, bool>> checkOncePredicates = new List<Func<ILexical, bool>>();
        Func<Sentence, IEnumerable<ILexical>> test {
            get {
                return val => {
                    var result = from v in val.Phrases where checkOncePredicates.All(f => f(v)) && predicates.All(f => f(v)) select v;
                    checkOncePredicates.Clear();
                    return result;
                };
            }
        }
        private IReadOnlyList<ILexical> values { get { return test(value).ToList(); } }
        protected bool Accepted { get; set; }

        public SentenceMatch TryPath<T1, T2, T3>(Action<T1, T2, T3> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(values);
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4);
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
                if (Accepted)
                    pattern(values[0] as T1, values[1] as T2, values[2] as T3, values[3] as T4, values[4] as T5,
                            values[5] as T6, values[6] as T7, values[7] as T8, values[8] as T9, values[9] as T10,
                            values[10] as T11, values[11] as T12, values[12] as T13, values[13] as T14, values[14] as T15,
                            values[15] as T16, values[16] as T17, values[17] as T18, values[18] as T19, values[19] as T20);
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