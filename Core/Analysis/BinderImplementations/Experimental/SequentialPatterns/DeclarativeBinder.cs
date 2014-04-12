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



    internal static class DeclarativeBinder
    {
        static void Test(Sentence s) {
            var b = new Test.BinderComponent() {
                (IEntity e1) => (IVerbal v) => (IEntity e2) => {
                    v.BindSubject(e1);
                    v.BindDirectObject(e2);
                },
                (IAdverbial a)=> (IDescriptor d) =>(IEntity e)=> {
                    e.BindDescriptor(d);
                    d.ModifyWith(a);
                },
                (IEntity e1)=> (IConjunctive c)=> (IVerbal v)=> (IAdverbial a)=>{ },
                (IEntity e1) => (IConjunctive c) => (IEntity e2) => (IVerbal v) => (IEntity e3) => (IPrepositional p1) => (IEntity e4) => {
                    c.JoinedLeft = e1;
                    c.JoinedRight = e2;
                    v.BindSubject(e1);
                    v.BindSubject(e2);
                    v.BindDirectObject(e3);
                    v.BindIndirectObject(e4);
                },
                (IEntity e1) => (IConjunctive c1) => (IEntity e2) => (IVerbal v) => (IEntity e3) => (IConjunctive c2) => (IEntity e4) => {
                    c1.JoinedLeft = e1;
                    c1.JoinedRight = e2;
                    v.BindSubject(e1);
                    v.BindSubject(e2);
                    c2.JoinedLeft = e1;
                    c2.JoinedRight = e2;
                    v.BindDirectObject(e3);
                    v.BindDirectObject(e4);
                },
                (IEntity e1) => (IVerbal v) => (IEntity e2) => (IConjunctive c) => (IEntity e3) => {
                    v.BindSubject(e1);
                    c.JoinedLeft = e1;
                    c.JoinedRight = e2;
                    v.BindDirectObject(e2);
                    v.BindDirectObject(e3);
                }
            };
        }


        internal static bool TryApply<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> description, IList<TLexical> elements)
                                  where T1 : class, ILexical
                                  where T2 : class, ILexical
                                  where T3 : class, ILexical
                                  where TLexical : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;
        }
        internal static bool TryApply<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> description, IList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> description, IList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> description, IList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> description, IList<TLexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
                  where TLexical : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> description, IList<TLexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
                where TLexical : class, ILexical {
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> description, IList<TLexical> elements)
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
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

        }
        internal static bool TryApply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> description, IList<TLexical> elements)
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
            var r = description.Applicable(elements); if (r) description.Apply(elements); return r;

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

        internal TestObject WithRule(BindingRule skipAdjectivals) {
            throw new NotImplementedException();
        }
    }

    internal class TestObject
    {
        public TestObject When(bool b) { return this; }

        internal object TryPath(Func<IEntity, object> p) {
            throw new NotImplementedException();
        }
    }

    static class TestTemp
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
    static class BindingHelpers
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