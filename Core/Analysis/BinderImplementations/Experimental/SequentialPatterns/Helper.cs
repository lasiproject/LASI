using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LASI;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.DocumentStructures;



namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{



    internal static class Helper
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
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, TLexical>(this Func<T1, Func<T2, Action<TLexical>>> description, IList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 2 &&
                elements[0] is T1 &&
                elements[1] is T2;
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
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, TLexical>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TLexical>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TLexical>>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TLexical>>>>>>> description, IList<TLexical> elements)
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
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TLexical>>>>>>>> description, IList<TLexical> elements)
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
    }
    internal static class Matcher
    {
        internal static SequenceMatch Match(this Sentence sentence) {
            return new SequenceMatch(sentence);
        }
    }
    class SequenceMatch(Sentence value)
    {
        protected IReadOnlyList<ILexical> Values { get; }
        = value.Phrases.ToList();
        protected bool Accepted { get; set; }

        public SequenceMatch Path<T1, T2, T3>(Action<T1, T2, T3> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical {
            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3);
            }
            return this;
        }
        public SequenceMatch Path<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical {
            if (pattern.Applicable(Values.ToList())) {
                Accepted = true;
                pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4);
            }
            return this;
        }
    }
    static class TestTemp
    {
        static void Test(Sentence value) {
            value.Match()
                .Path((IEntity e, IVerbal v, IReferencer r) => {
                })
                .Path((IReferencer r, IVerbal v1, IEntity e1, IEntity e2) => {
                });
        }

    }
    class Pattern
    {

    }
}



