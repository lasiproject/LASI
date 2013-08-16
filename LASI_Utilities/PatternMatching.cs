/*
 * Concepts and tutorials for these customized switch statement functions were provided 
 * by Bart De Smet via his exceptional programming blog. 
 * Entry: A FUNCTIONAL C# (TYPE)SWITCH  http://community.bartdesmet.net/blogs/bart/archive/2008/03/30/a-functional-c-type-switch.aspx
 * Main Blog: http://community.bartdesmet.net/blogs/bart/default.aspx
 * Thank you!
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Utilities.PatternMatching
{

    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    public static class MatchingExtensions
    {
        public static PatternMatching<T> Match<T>(this T t) where T : class { return new PatternMatching<T>(t); }
        //public static PatternToFromTransition<T> MatchFrom<T>(this T t) where T : class { return new PatternToFromTransition<T>(t); }
        //public static M<object, R> MatchTo<R>(this object t) { return new M<object, R>(t); }
    }
    public static class Match
    {
        public static PatternMatching<T> On<T>(T matchOn) where T : class { return new PatternMatching<T>(matchOn); }
        public static PatternMatching<T, R> On<T, R>(T matchOn) where T : class { return new PatternMatching<T, R>(matchOn); }
        public static PatternToFromTransition<T> From<T>(T matchOn) where T : class { return new PatternToFromTransition<T>(matchOn); }
    }
    public class PatternMatching<T, R> where T : class
    {
        protected internal PatternMatching(T matchOn) { toMatch = matchOn; }
        public PatternMatching<T, R> With<TCase>(Func<R> func) where TCase : class,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    result = func();
                    matchFound = true;
                }
            }
            return this;
        }
        public PatternMatching<T, R> With<TCase>(Func<TCase, bool> condition, Func<R> func) where TCase : class,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null && condition(matched)) {
                    result = func();
                    matchFound = true;
                }
            }
            return this;
        }
        public PatternMatching<T, R> With<TCase>(Func<TCase, R> func) where TCase : class,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    result = func(matched);
                    matchFound = true;
                }
            }
            return this;
        }
        public PatternMatching<T, R> With<TCase>(Func<TCase, bool> condition, Func<TCase, R> func) where TCase : class,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null && condition(matched)) {
                    result = func(matched);
                    matchFound = true;
                }
            }
            return this;
        }
        /// <summary>
        /// Specifies a default result to yield when no patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The M&lgt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public PatternMatching<T, R> Default(Func<R> func) {
            if (!matchFound) {
                result = func();
                matchFound = true;
            }
            return this;
        }
        public PatternMatching<T, R> Default(Func<T, R> func) {
            if (!matchFound) {
                result = func(toMatch);
                matchFound = true;
            }
            return this;
        }
        public R Result() { return result; }

        private bool matchFound;
        private T toMatch;
        private R result;
    }
    public class PatternMatching<T> where T : class
    {

        protected internal PatternMatching(T matchOn) { toMatch = matchOn; }
        public PatternMatching<T> With<TCase>(Action action) where TCase : class ,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    matchFound = true;
                    action();
                }
            }
            return this;
        }
        public PatternMatching<T> With<TCase>(Action<TCase> action) where TCase : class ,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    matchFound = true;
                    action(matched);
                }
            }
            return this;
        }
        public PatternMatching<T> With<TCase>(Func<TCase, bool> condition, Action action) where TCase : class ,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    matchFound = true;
                    action();
                }
            }
            return this;
        }
        public PatternMatching<T> With<TCase>(Func<TCase, bool> condition, Action<TCase> action) where TCase : class ,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    matchFound = true;
                    action(matched);
                }
            }
            return this;
        }
        public void Default(Action action) {
            if (!matchFound) {
                action();
            }
        }
        public void Default(Action<T> action) {
            if (!matchFound) {
                action(toMatch);
            }
        }
        private bool matchFound;
        private T toMatch;

    }
    public struct PatternToFromTransition<T> where T : class
    {
        internal PatternToFromTransition(T toMatch) {
            matchOn = toMatch;
        }
        public PatternMatching<T, R> To<R>() {
            return new PatternMatching<T, R>(matchOn);
        }
        private T matchOn;
    }




    static class IEnumerableMatchingExtensions
    {
        static IEnumerable<R> PatternMatch<T, R>(IEnumerable<T> elements)
            where T : class
            where R : class,T {
            return from e in elements
                   select Match.From<T>(e).To<R>()
                          .With<T>(x => x as R)
                          .With<R>(x => x != e, r => r)
                          .Result() into result
                   where result != null
                   select result;
        }
    }
}