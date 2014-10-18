using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.PatternMatching
{
    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions 
    /// which match with a value of Type T and does not yield a result.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam> 
    /// <remarks>
    /// <para>
    /// It is important to note that null values are coalesced by the Match process. 
    /// That is to say that if the value being tested is null it will never match any of the Clauses.
    /// Because of this a null value will always yield the default result and never produce an error.
    /// </para>
    /// <para>
    /// The type based pattern matching functionality was introduced to solve the problem of performing subtype dependent operations which could not be described by traditional virtual method approaches in an expressive or practical manner.
    /// There are several reasons for this. First and most important, is that the virtual method approach is limited to single dispatch* . Secondly different binding operations must be chosen based on a variety of information gathered from contexts 
    /// which are often external to the single instance of the type representing a lexical construct. Different numbers and types of arguments that depend on the surrounding context of the lexical instnace cannot be specified in a manner compatible 
    /// with virtual method signatures. However, a linear storage and traversal mechanism for lexical elements of differing syntactic types, as well as the need to define types representing elements having overlapping syntactic roles,
    /// remains a necessity which prevents the complete stratification of class and interface hierarchies.
    /// There were a variety of solutions that were considered:
    /// </para>
    /// <para>
    /// 1. A visitor pattern could be implemented such that the an object traversing a sequence of lexical elements would be passed in turn to each, and by carrying with it sufficient and arbitrary context 
    ///     and by providing an overload for every syntactic type, could provide functionality to implement operations between elements. There are several drawbacks involved including increased state disperal, increased class coupling,
    ///     the maintenance cost of re factoring types, the need to define new classes, which carry arbitrary algorithm state with them but must exist in their own hierarchy, and other factors generally increasing complexity.
    /// </para>
    /// <para>
    /// 2. A more flexible, visitor-like pattern could be implemented by taking advantage of dynamic dispatch and runtime overload resolution.
    ///     This has the drawback of the code not clearly expressing its semantics statically as it essentially relies on the runtime to describe control flow, never stating it explicitly.
    /// </para>
    /// <para>
    /// 3. Describe traversal algorithms using complex, nested tiers of conditional blocks determined by the results of type casts. This is error prone, unwieldy, ugly, and obscures the logic with noise. Additionally this approach may be inconsistently applied in different section of code, causing the implementations of algorithms so written to be prone to subtle errors**.  
    /// </para><para>
    /// The pattern matching implemented here abstracts over the type checking logic, allowing it to be tested and optimized for the large variety of algorithms in need of such functionality, 
    /// allows for subtype constraints to be specified, allows for algorithms to handle as many or as few types as needed, and emphasizes the intent of the code which leverage it. 
    /// It also eliminates the difficulty of defining state-full visitors by defining a match with a particular subtype as a function on that type. Such a function is intuitively specified as an anonymous closure which
    /// implicitly captures any state it will use. This approach also encourages the localization of logic, which in the case of visitors would not possible as implementations of visitors will inherently get spread out in both the textual space of the source code and the conceptions of implementers.
    /// The syntax for pattern matching uses a fluent interface style.
    /// </para>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match()
    ///         .With&lt;IReferencer&gt;(r => Console.WriteLine(r.ReferredTo.Weight))
    ///     	.With&lt;IEntity&gt;(e => Console.WriteLine(e.Weight))
    ///     	.With&lt;IVerbal&gt;(v =>  Console.WriteLine(v.HasSubject()? v.Subject.Weight : 0));
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match()
    ///			.With&lt;Phrase&gt;(p => Console.WriteLine(p.Words.Average(w => w.Weight)))
    ///			.With&lt;Word&gt;(w => Console.WriteLine(w.Weight))
    ///     .Default(()=> Console.WriteLine("not a word or phrase"))
    /// </code>
    /// </example>
    /// <para>
    /// Patterns may be nested arbitrarily as in the following example
    /// </para>
    /// <para>
    /// When a Yield clause is not applied, the Match Expression will not yield a value. Instead it will behave much as a Type driven switch statement. 
    /// <example>
    /// <code>
    /// myLexical.Match()
    ///         .With&lt;Phrase&gt;(p => Console.WriteLine(&quot;Phrase: &quot;, p.Text))
    ///		    .With&lt;Word&gt;(w => Console.WriteLine(&quot;Word: &quot;, w.Text))
    ///	    .Default(() => Console.WriteLine(&quot;Not a Word or Phrase&quot;));
    /// </code>
    /// </example>
    /// </para>
    /// <para>
    /// * a: The visitor pattern provides statically type safe double dispatch, at the cost of increased code complexity and increased class coupling.
    /// b: As LASI is implemented using C# 5.0, it has access to the language's built in support for truly dynamic multi-methods with arbitrary numbers of arguments.
    /// However while experimenting with this approach, in a constrained scope and environment involving a fixed set of method overloads, this approach still had the drawbacks
    ///	 of reducing type safety, making extensions to type hierarchies potentially volatile, and drastically harming readability and maintainability.
    ///	 </para><para>
    ///	 ** C# offers several  methods of type checking, type casting, and type conversions, each with distinct semantics and sometimes drastically different performance characteristics.
    ///	This is justification enough to form a centralized API and design pattern within the context of the project.(for example: if one algorithm is implemented using 
    ///	as/is operator semantics, which do not consider user defined conversions, it will not naturally adjust if the such conversions are defined)
    /// </para>
    /// </remarks>  
    /// <see cref="LASI.Core.Matcher">Provides extension methods which allow for the creation of Match expressions.</see>
    [DebuggerStepThrough]
    public class Match<T> : MatchBase<T> where T : class, ILexical
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Match&lt;T&gt; class which will match against the supplied value.
        /// </summary>
        /// <param name="value">The value to match against.</param>
        internal Match(T value) : base(value) { }
        #endregion

        #region Expression Transformations

        /// <summary>
        /// Promotes the current non result returning expression of type Case&lt;T&gt; into a result returning expression of With&lt;T, R&gt;
        /// Such that subsequent With expressions appended are now to yield a result value of the supplied Type R.
        /// </summary>
        /// <typeparam name="TResult">The Type of the result which the match expression may now return.</typeparam>
        /// <returns>A Match&lt;T, R&gt; representing the now result yielding Match expression.</returns> 
        public Match<T, TResult> Yield<TResult>() {
            return new Match<T, TResult>(Value);
        }


        #endregion

        #region When Expressions
        /// <summary>
        /// Appends a When expression to the current PatternMatching Expression. The When expression applies a predicate to the value being matched over. 
        /// It must be followed by a Then expression which is only considered if the predicate applied here returns true.
        /// </summary>
        /// <param name="predicate">The predicate to test the value being matched over.</param>
        /// <returns>The PredicatedMatch&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public PredicatedMatch<T> When(Func<T, bool> predicate) {
            return new PredicatedMatch<T>(predicate(Value), this);
        }

        /// <summary>
        /// Appends a When expression to the current PatternMatching Expression. The When expression applies a predicate to the value being matched over. 
        /// It must be followed by a Then expression which is only considered if the predicate applied here returns true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. That the value being matched is of this type is also necessary for the following then expression to be selected.</typeparam>
        /// <param name="predicate">The predicate to test the value being matched over.</param>
        /// <returns>The PredicatedMatch&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public PredicatedMatch<T> When<TPattern>(Func<TPattern, bool> predicate) where TPattern : class, ILexical {
            var typed = Value as TPattern;
            return new PredicatedMatch<T>(typed != null && predicate(typed), this);
        }

        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched such that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="condition">The predicate to test the value being matched.</param>
        /// <returns>The PredicatedMatch&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression</returns>
        public PredicatedMatch<T> When(bool condition) {
            return new PredicatedMatch<T>(condition, this);
        }

        #endregion

        #region With Expressions

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> With<TPattern>(Action action) where TPattern : class, ILexical {

            if (!Accepted && Value is TPattern) {
                Accepted = true;
                action();
            }
            return this.With((TPattern t) => action());
        }

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action&lt;TPattern&gt; which, if this Case expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> With<TPattern>(Action<TPattern> action) where TPattern : class, ILexical {
            if (Value != null) {
                if (!Accepted) {
                    var matched = Value as TPattern;
                    if (matched != null) {
                        Accepted = true;
                        action(matched);
                    }
                }
            }
            return this;
        }


        #endregion

        #region Default Expressions

        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke if no matches in the expression succeeded.</param>
        public void Default(Action action) {
            if (!Accepted)
                action();
        }
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke on the match Case value if no matches in the expression succeeded.</param>
        public void Default(Action<T> action) {
            if (!Accepted && Value != null) {
                action(Value);
            }
        }

        #endregion


        #region Operators 
        //public static implicit operator bool(Match<T> match) { return match.Accepted; }
        #endregion

    }
    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions from a match over a value of Type T to a result of Type TResult.
    /// If no Type is matched, the result will be the default value for the Type TResult. 
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    /// <typeparam name="TResult">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    ///<remarks>
    /// <para>
    /// The type based pattern matching functionality was introduced to solve the problem of performing subtype dependent operations which could not be described by traditional virtual method approaches in an expressive or practical manner.
    /// There are several reasons for this. First and most important, is that the virtual method approach is limited to single dispatch* . Secondly different binding operations must be chosen based on a variety of information gathered from contexts 
    /// which are often external to the single instance of the type representing a lexical construct. Different numbers and types of arguments that depend on the surrounding context of the lexical instnace cannot be specified in a manner compatible 
    /// with virtual method signatures. However, a linear storage and traversal mechanism for lexical elements of differing syntactic types, as well as the need to define types representing elements having overlapping syntactic roles,
    /// remains a necessity which prevents the complete stratification of class and interface hierarchies.
    /// There were a variety of solutions that were considered:
    /// </para>
    /// <para>
    /// 1. A visitor pattern could be implemented such that the an object traversing a sequence of lexical elements would be passed in turn to each, and by carrying with it sufficient and arbitrary context 
    ///     and by providing an overload for every syntactic type, could provide functionality to implement operations between elements. There are several drawbacks involved including increased state disperal, increased class coupling,
    ///     the maintenance cost of re factoring types, the need to define new classes, which carry arbitrary algorithm state with them but must exist in their own hierarchy, and other factors generally increasing complexity.
    /// </para>
    /// <para>
    /// 2. A more flexible, visitor-like pattern could be implemented by taking advantage of dynamic dispatch and runtime overload resolution.
    ///     This has the drawback of the code not clearly expressing its semantics statically as it essentially relies on the runtime to describe control flow, never stating it explicitly.
    /// </para>
    /// <para>
    /// 3. Describe traversal algorithms using complex, nested tiers of conditional blocks determined by the results of type casts. This is error prone, unwieldy, ugly, and obscures the logic with noise. Additionally this approach may be inconsistently applied in different section of code, causing the implementations of algorithms so written to be prone to subtle errors**.  
    /// </para><para>
    /// The pattern matching implemented here abstracts over the type checking logic, allowing it to be tested and optimized for the large variety of algorithms in need of such functionality, 
    /// allows for subtype constraints to be specified, allows for algorithms to handle as many or as few types as needed, and emphasizes the intent of the code which leverage it. 
    /// It also eliminates the difficulty of defining state-full visitors by defining a match with a particular subtype as a function on that type. Such a function is intuitively specified as an anonymous closure which
    /// implicitly captures any state it will use. This approach also encourages the localization of logic, which in the case of visitors would not possible as implementations of visitors will inherently get spread out in both the textual space of the source code and the conceptions of implementers.
    /// The syntax for pattern matching uses a fluent interface style.
    /// </para>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///         .With((IReferencer r) => r.ReferredTo.Weight)
    ///     	.With((IEntity e) => e.Weight)
    ///     	.With((IVerbal v) => v.HasSubject()? v.Subject.Weight : 0)
    ///		.Result(1);	
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///			.With((Phrase p) => p.Words.Average(w => w.Weight))
    ///			.With((Word w) => w.Weight)
    ///		.Result();
    /// </code>
    /// </example>
    /// <para>
    /// Patterns may be nested arbitrarily as in the following example
    /// </para>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///         .With((IReferencer r) => r.ReferredTo
    ///             .Match().Yield&lt;double&gt;()
    ///                 .With((Phrase p) => p.Words.OfNoun().Average(w => w.Weight))
    ///             .Result())
    ///         .With((Noun n) => n.Weight)
    ///     .Result();
    /// </code>
    /// </example>
    /// <para>
    /// When a Yield clause is not applied, the Match Expression will not yield a value. Instead it will behave much as a Type driven switch statement. 
    /// <example>
    /// <code>
    /// myLexical.Match()
    ///         .With((Phrase p) => Console.Write(&quot;Phrase: &quot;, p.Text))
    ///		    .With((Word w) => Console.Write(&quot;Word: &quot;, w.Text))
    ///	    .Default(() => Console.Write(&quot;Not a Word or Phrase&quot;));
    /// </code>
    /// </example>
    /// </para>
    /// <para>
    /// * a: The visitor pattern provides statically type safe double dispatch, at the cost of increased code complexity and increased class coupling.
    /// b: As LASI is implemented using C# 5.0, it has access to the language's built in support for truly dynamic multi-methods with arbitrary numbers of arguments.
    /// However while experimenting with this approach, in a constrained scope and environment involving a fixed set of method overloads, this approach still had the drawbacks
    ///	 of reducing type safety, making extensions to type hierarchies potentially volatile, and drastically harming readability and maintainability.
    ///	 </para><para>
    ///	 ** C# offers several  methods of type checking, type casting, and type conversions, each with distinct semantics and sometimes drastically different performance characteristics.
    ///	This is justification enough to form a centralized API and design pattern within the context of the project.(for example: if one algorithm is implemented using 
    ///	as/is operator semantics, which do not consider user defined conversions, it will not naturally adjust if the such conversions are defined)
    /// </para>
    /// </remarks>
    [DebuggerStepThrough]
    public class Match<T, TResult> : MatchBase<T> where T : class, ILexical
    {
        #region Constructors

        /// <summary>
        /// Initailizes a new instance of the With&lt;T,R&gt; which will allow for Pattern Matching with the provided value.
        /// </summary>
        /// <param name="value">The value to match with.</param>
        internal Match(T value) : base(value) { }
        #endregion

        #region When Expressions
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched such that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="predicate">The predicate to test the value being matched.</param>
        /// <returns>The PredicatedMatch&lt;T, R&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public PredicatedMatch<T, TResult> When(Func<T, bool> predicate) {
            return new PredicatedMatch<T, TResult>(predicate(Value), this);
        }
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched such that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. That the value being matched is of this type is also necessary for the following then expression to be selected.</typeparam>
        /// <param name="predicate">The predicate to test the value being matched.</param>
        /// <returns>The PredicatedMatch&lt;T, R&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public PredicatedMatch<T, TResult> When<TPattern>(Func<TPattern, bool> predicate) where TPattern : class, ILexical {
            var typed = Value as TPattern;
            return new PredicatedMatch<T, TResult>(typed != null && predicate(typed), this);
        }
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched such that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="condition">The predicate to test the value being matched.</param>
        /// <returns>The PredicatedMatch&lt;T, R&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public PredicatedMatch<T, TResult> When(bool condition) {
            return new PredicatedMatch<T, TResult>(condition, this);
        }
        #endregion

        #region With Expressions
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match With TPattern.</param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> With<TPattern>(Func<TResult> func) where TPattern : class, ILexical {
            if (Value != null && !Accepted && Value is TPattern) {
                result = func();
                Accepted = true;
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">
        /// The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.
        /// </typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched
        /// with to produce the desired result for a Match with TPattern.
        /// </param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns> 
        public Match<T, TResult> With<TPattern>(Func<TPattern, TResult> func) where TPattern : class, ILexical {
            if (!Accepted && Value != null) {
                var matched = Value as TPattern;
                if (matched != null) {
                    result = func(matched);
                    Accepted = true;
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="result">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> With<TPattern>(TResult result) where TPattern : class, ILexical {
            if (!Accepted && Value != null) {
                var matched = Value as TPattern;
                if (matched != null) {
                    this.result = result;
                    Accepted = true;
                }
            }
            return this;
        }
        #endregion

        #region Result Expressions
        /// <summary>
        /// Returns the result of the Pattern Matching expression. This will be either the result specified for the first Match expression which succeeded or the default value the type TResult. 
        /// </summary>
        /// <returns>The result of the Pattern Matching expression.</returns>
        public TResult Result() { return result; }
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The result of the first successful match or the value given by invoking the supplied factory function.</returns>

        public TResult Result(Func<TResult> func) {
            if (!Accepted) {
                result = func();
                Accepted = true;
            }
            return result;
        }
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The result of the first successful match or the value given by invoking the supplied factory function.</returns>
        public TResult Result(Func<T, TResult> func) {
            if (Value != null && !Accepted) {
                result = func(Value);
                Accepted = true;
            }
            return result;
        }
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched. 
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The result of the first successful match or supplied default value.</returns>
        public TResult Result(TResult defaultValue) {
            if (!Accepted) {
                result = defaultValue;
                Accepted = true;
            }
            return result;
        }


        #endregion

        #region Fields
        private TResult result = default(TResult);
        #endregion
    }
}