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


        internal static bool Applicable<T1, T2, T3, TLexical>(this Action<T1, T2, T3> description, IList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Action<T1, T2, T3, T4> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Action<T1, T2, T3, T4, T5> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Action<T1, T2, T3, T4, T5, T6> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7> description, IList<TLexical> elements)
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

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> description, IList<TLexical> elements)
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
            return elements.Count >= 8 &&
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

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> description, IList<TLexical> elements)
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
                     where TLexical : class, ILexical {
            return elements.Count >= 8 &&
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
        internal static bool Applicable<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> description, IList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> description, IList<TLexical> elements)
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



        internal static void Apply<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> description, IList<TLexical> elements)
                                  where T1 : class, ILexical
                                  where T2 : class, ILexical
                                  where T3 : class, ILexical
                                  where TLexical : class, ILexical {
            description(elements[0] as T1)(elements[1] as T2)(elements[2] as T3);
        }
        internal static void Apply<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> description, IList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            description(
                    elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4);
        }
        internal static void Apply<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> description, IList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical {
            description(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> description, IList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical {
            description(
               elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> description, IList<TLexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
                  where TLexical : class, ILexical {
            description(
                elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> description, IList<TLexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
                where TLexical : class, ILexical {
            description(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> description, IList<TLexical> elements)
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
            description(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8)(
                elements[8] as T9);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> description, IList<TLexical> elements)
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
            description(
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

        internal static bool TryApply<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> description, IList<ILexical> elements)
                                where T1 : class, ILexical
                                where T2 : class, ILexical
                                where T3 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;
        }
        internal static bool TryApply<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> description, IList<ILexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> description, IList<ILexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> description, IList<ILexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> description, IList<ILexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> description, IList<ILexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> description, IList<ILexical> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> description, IList<ILexical> elements)
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
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> description, IList<Phrase> elements)
                               where T1 : class, ILexical
                               where T2 : class, ILexical
                               where T3 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;
        }
        internal static bool TryApply<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> description, IList<Phrase> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> description, IList<Phrase> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> description, IList<Phrase> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> description, IList<Phrase> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> description, IList<Phrase> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> description, IList<Phrase> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> description, IList<Phrase> elements)
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
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }

    }
    internal static class Matcher
    {
        internal static SentenceMatch Match(this Sentence sentence) {
            return new SentenceMatch(sentence);
        }
    }
    public partial class SentenceMatch(Sentence value)
    {
        protected IReadOnlyList<ILexical> Values { get; }
        = value.Phrases.ToList();
        protected bool Accepted { get; set; }

        public SentenceMatch TryPath<T1, T2, T3>(Action<T1, T2, T3> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical {
            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3);
            }
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical {
            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4);
            }
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical {

            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5);
            }
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical {

            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6);
            }
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> pattern)
    where T1 : class, ILexical
    where T2 : class, ILexical
    where T3 : class, ILexical
    where T4 : class, ILexical
    where T5 : class, ILexical
    where T6 : class, ILexical
    where T7 : class, ILexical {

            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7);
            }
            return this;
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

            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8);
            }
            return this;
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

            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9);
            }
            return this;
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

            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10);
            }
            return this;
        }

        public SentenceMatch TryPath<T1, T2, T3>(Func<T1, Func<T2, Action<T3>>> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());

            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4>(Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5>(Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern)
    where T1 : class, ILexical
    where T2 : class, ILexical
    where T3 : class, ILexical
    where T4 : class, ILexical
    where T5 : class, ILexical
    where T6 : class, ILexical
    where T7 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern)
    where T1 : class, ILexical
    where T2 : class, ILexical
    where T3 : class, ILexical
    where T4 : class, ILexical
    where T5 : class, ILexical
    where T6 : class, ILexical
    where T7 : class, ILexical
    where T8 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern)
    where T1 : class, ILexical
    where T2 : class, ILexical
    where T3 : class, ILexical
    where T4 : class, ILexical
    where T5 : class, ILexical
    where T6 : class, ILexical
    where T7 : class, ILexical
    where T8 : class, ILexical
    where T9 : class, ILexical {
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern)
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
            Accepted = pattern.TryApply(Values.ToList());
            return this;
        }

        internal SentenceMatch WithRule(BindingRule rule) {
            return this;
        }
    }



    class Pattern
    {
        void BindSentence(Sentence s) {
            // wrap the sentence with a match that tries each path
            //s.Match()
            //    .WithRule(BindingRule.SkipAdjectivals) // Adjectivals which appear within the attempted paths will be ignored.
            //    .When(s.Phrases.OfVerbPhrase().Any()) //just example of an arbitrary condition to check.
            //.TryPath(
            //    (IEntity e1) => (IConjunctive c1) => (IEntity e2) => (IVerbal v1) => (IEntity e3) => (IPrepositional p1) => (IEntity e4) => {
            //        c1.JoinedLeft = e1;
            //        c2.JoinedRight = e2;
            //        v1.BindSubject(e1);
            //        v1.BindSubject(e2);
            //        v1.BindDirectObject(e3);
            //        v2.BindIndirectObject(e4);
            //        LearingMachine.UpdateStatistics(e1, c1, e2, v1, e3, p1, e4);
            //    })
            //.WithRule(BindingRule.EmphasizeNamedEntities)
            //.TryPath(
            //*/
            //);

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
        public static bool Test(this BindingRule rule, ILexical value) {
            switch (rule) {
                default:
                    return true;
            }
        }
    }
    enum BindingRule
    {
        EmphasizeNamedEntities,
        SkipAdjectivals
    }
}