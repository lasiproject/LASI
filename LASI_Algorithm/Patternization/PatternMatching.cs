using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm.Patternization
{
    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    ///<remarks>
    /// The type based pattern matching functionality was introduced to solve the problem of performing subtype dependent operations which could be neither conceptually nor practically described by the traditional virtual method opproach.
    /// There are several reasons for this. Firstly, and most importantly, the virtual method approach is limited to single dispatch. Secondly different binding operations must be chosen based on a variety of information gathered from contexts 
    /// which are often external to a single instance of a type representing a lexical construct. Different numbers and types of arguments that depend on the surrounding context to the cannot be specified in a manner compatable 
    /// with virtual method signatures. However, a linear storage and traversal mechanism for lexical elements of differing syntactic types, as well as the need to define types representing elements having overlapping syntactic roles,
    /// remains a necessity which prevents the complete stratification of class and interface heirarchies.
    /// There were a variety of solutions that were considered:
    /// 
    /// 1. A visitor pattern could be implemented such that the an object traversing a sequence of lexical elements would be passed in turn to each,and by carrying with it sufficient and arbitary context 
    ///     and by providing an overload for every syntactic type, could provide functionality to implement operations between elements. There are several drawbacks involved including increased state, increased class coupling,
    ///     the mantenance cost of refactoring types, the need to define new classes, which carry arbitrary algorithm state with them but must exist in their own heirarchy, and is generally heavy weight.
    /// 2. A more flexible, visitor-like pattern could be implemented by taking advantage of dynamic dispatch and runtime overload resolution.
    ///     This has the drawback of the code not clearly expressing its semantics statically as it is essentially relying on the runtime to describe control flow, never stating it explicitely.
    /// 3. Describe traversal algorithms using complex, nested teirs of conditional blocks determined by the results of type casts. This is error prone, unweildy, ugly, and obscures the logic with noise.
    /// 
    /// The pattern matching implemented here abstracts over the type checking logic, allowing it to be tested and optemized for the large variety of algorithms in need of such functionality, 
    /// allows for subtype constraints to be specified, allows for algorithms to handle as many or as few types as needed, and emphasizes the intent of the code which leverages it. 
    /// It also elminates the difficulty of defining statefull visitors by defining a match with a particular subtype as a function on that type. Such a function is intuitively specified as an anonymous closure which
    /// implicitely captures any state it will use. This approach also encourage the localization of logic, which in the case of visitors would often need to be spread over several classes for complex algorithms.
    ///</remarks>
    public static class Matcher
    {
        /// <summary>
        /// Constructs the head of a non result returning Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="matchOn">The value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression with the specified value.</returns>
        public static MatchCase<T> MatchOne<T>(this T matchOn) where T : class , ILexical { return new MatchCase<T>(matchOn); }
        /// <summary>
        /// Constructs the head of a result-yielding, Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <typeparam name="R">The Type of the result to be yielded.</typeparam> 
        /// <param name="matchOn">The value to match with.</param>     
        /// <returns>The head of a result-yielding, Type based Pattern Matching expression with the specified value.</returns>
        public static MatchCase<T, R> MatchTo<T, R>(T matchOn) where T : class , ILexical { return new MatchCase<T, R>(matchOn); }
        /// <summary>
        /// Constructs the intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="matchOn">The value to match with.</param>
        /// <returns>The intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </returns>
        public static FromTo<T> From<T>(T matchOn) where T : class, ILexical { return new FromTo<T>(matchOn); }
        /// <summary>
        /// Represents the intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value that will be matched with.</typeparam>
        public struct FromTo<T> where T : class, ILexical
        {
            internal FromTo(T toMatch) {
                matchOn = toMatch;
            }
            /// <summary>
            /// Completes the From...To expression by specifying the type of the Result and constructing and returning the head of a result-yielding, Type based Pattern Matching expression.
            /// </summary>
            /// <typeparam name="R">The Type of the Results which may be returned by the expressions appended to the newly created expression.</typeparam>
            /// <returns>
            ///  The head of a result-yielding, Type based Pattern Matching expression.
            ///  </returns>  
            public MatchCase<T, R> To<R>() {
                return new MatchCase<T, R>(matchOn);
            }
            private T matchOn;
        }
    }

    public interface IMatchCase<T, R> { R Result(); }

    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions from a match with value of Type T to a Result of Type R.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    /// <typeparam name="R">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    public class MatchCase<T, R> : IMatchCase<T, R> where T : class, ILexical
    {
        /// <summary>
        /// Initailizes a new instance of the PatternMatching&lt;T&gt;&lt;R&gt; which will allow for Pattern Matching with the provided value.
        /// </summary>
        /// <param name="matchOn">The value to match with.</param>
        protected internal MatchCase(T matchOn) { toMatch = matchOn; }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<R> func) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    if (toMatch is TCase) {
                        result = func();
                        matchFound = true;
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="condition">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> condition, Func<R> func) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null && condition(matched)) {
                        result = func();
                        matchFound = true;
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, R> func) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null) {
                        result = func(matched);
                        matchFound = true;
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="condition">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> condition, Func<TCase, R> func) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null && condition(matched)) {
                        result = func(matched);
                        matchFound = true;
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="caseResult">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(R caseResult) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null) {
                        result = caseResult;
                        matchFound = true;
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="condition">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="caseResult">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> condition, R caseResult) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null && condition(matched)) {
                        result = caseResult;
                        matchFound = true;
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public IMatchCase<T, R> Default(Func<R> func) {
            if (!matchFound) {
                result = func();
                matchFound = true;
            }
            return this;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public IMatchCase<T, R> Default(Func<T, R> func) {
            if (toMatch != null) if (!matchFound) {
                    result = func(toMatch);
                    matchFound = true;
                }
            return this;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public IMatchCase<T, R> Default(R defaultValue) {
            if (!matchFound) {
                result = defaultValue;
                matchFound = true;
            }
            return this;
        }
        /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>Returns the result of the Pattern Matching expression.</returns>
        public R Result() { return result; }


        /// <summary>
        /// Gets a value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool matchFound;
        private T toMatch;
        private R result = default(R);
    }


    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions which match with a value of Type T and does not yield a result.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam> 
    public class MatchCase<T> : LASI.Algorithm.Patternization.IMatchCase<T> where T : class,ILexical
    {
        internal MatchCase(T matchOn) { toMatch = matchOn; }
        public MatchCase<T, R> Yield<R>() { return new MatchCase<T, R>(this.toMatch); }
        public IMatchCase<T> With<TCase>(Action action) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    if (toMatch is TCase) {
                        matchFound = true;
                        action();
                    }
                }
            return this;
        }
        public IMatchCase<T> With<TCase>(Action<TCase> action) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null) {
                        matchFound = true;
                        action(matched);
                    }
                }
            return this;
        }
        public IMatchCase<T> With<TCase>(Func<TCase, bool> condition, Action action) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    if (toMatch is TCase) {
                        matchFound = true;
                        action();
                    }
                }
            return this;
        }
        public IMatchCase<T> With<TCase>(Func<TCase, bool> condition, Action<TCase> action) where TCase : class, ILexical {
            if (toMatch != null) if (!matchFound) {
                    var matched = toMatch as TCase;
                    if (matched != null) {
                        matchFound = true;
                        action(matched);
                    }
                }
            return this;
        }
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke if no matches in the expression succeeded.</param>
        public void Default(Action action) {
            if (!matchFound) {
                action();
            }
        }
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke on the match with value if no matches in the expression succeeded.</param>
        public void Default(Action<T> action) {
            if (toMatch != null) if (!matchFound) {
                    action(toMatch);
                }
        }

        /// <summary>
        /// The value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool matchFound;
        private T toMatch;

    }

}