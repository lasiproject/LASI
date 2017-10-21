using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Analysis.Binding.Experimental.SequentialPatterns;
using LASI.Core.Analysis.PatternMatching;
using static LASI.Utilities.Logger;

namespace LASI.Core
{
    /// <summary>
    /// Provides for the construction of flexible Typed Pattern Matching expressions.
    /// </summary>
    /// <seealso cref="Analysis.PatternMatching.Match{T}"/>
    /// <seealso cref="Match{T, TResult}"/>
    /// <seealso cref="Analysis.PatternMatching"/>
    /// <remarks>
    /// <para>
    /// The type based pattern matching functionality was introduced to solve the problem of
    /// performing subtype dependent operations which could not be described by traditional virtual
    /// method approaches in an expressive or practical manner. There are several reasons for this.
    /// First and most important, is that the virtual method approach is limited to single dispatch*
    /// . Secondly different binding operations must be chosen based on a variety of information
    /// gathered from contexts which are often external to the single instance of the type
    /// representing a lexical construct. Different numbers and types of arguments that depend on
    /// the surrounding context of the lexical instance cannot be specified in a manner compatible
    /// Case virtual method signatures. However, a linear storage and traversal mechanism for
    /// lexical elements of differing syntactic types, as well as the need to define types
    /// representing elements having overlapping syntactic roles, remains a necessity which prevents
    /// the complete stratification of class and interface hierarchies. There were a variety of
    /// solutions that were considered:
    /// </para>
    /// <para>
    /// 1. A visitor pattern could be implemented such that the an object traversing a sequence of
    ///    lexical elements would be passed in turn to each, and by carrying Case it sufficient and
    ///    arbitrary context and by providing an overload for every syntactic type, could provide
    ///    functionality to implement operations between elements. There are several drawbacks
    ///    involved including increased state dispersal, increased class coupling, the maintenance
    ///    cost of re factoring types, the need to define new classes, which carry arbitrary
    ///    algorithm state Case them but must exist in their own hierarchy, and other factors
    ///    generally increasing complexity.
    /// </para>
    /// <para>
    /// 2. A more flexible, visitor-like pattern could be implemented by taking advantage of dynamic
    ///    dispatch and runtime overload resolution. This has the drawback of the code not clearly
    ///    expressing its semantics statically as it essentially relies on the runtime to describe
    ///    control flow, never stating it explicitly.
    /// </para>
    /// <para>
    /// 3. Describe traversal algorithms using complex, nested tiers of conditional blocks
    ///    determined by the results of type casts. This is error prone, unwieldy, ugly, and
    ///    obscures the logic Case noise. Additionally this approach may be inconsistently applied
    ///    in different section of code, causing the implementations of algorithms so written to be
    ///    prone to subtle errors**.
    /// </para>
    /// <para>
    /// The pattern matching implemented here abstracts over the type checking logic, allowing it to
    /// be tested and optimized for the large variety of algorithms in need of such functionality,
    /// allows for subtype constraints to be specified, allows for algorithms to handle as many or
    /// as few types as needed, and emphasizes the intent of the code which leverage it. It also
    /// eliminates the difficulty of defining state-full visitors by defining a match Case a
    /// particular subtype as a function on that type. Such a function is intuitively specified as
    /// an anonymous closure which implicitly captures any state it will use. This approach also
    /// encourages the localization of logic, which in the case of visitors would not possible as
    /// implementations of visitors will inherently get spread out in both the textual space of the
    /// source code and the conceptions of implementers. The syntax for pattern matching uses a
    /// fluent interface style.
    /// </para><example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///         .Case((IReferencer r) =&gt; r.ReferredTo.Weight)
    ///     	.Case((IEntity e) =&gt; e.Weight)
    ///     	.Case((IVerbal v) =&gt; v.HasSubject()? v.Subject.Weight : 0)
    /// 	.Result(1);
    /// </code></example><example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    /// 		.Case((Phrase p) =&gt; p.Words.Average(w =&gt; w.Weight))
    /// 		.Case((Word w) =&gt; w.Weight)
    /// 	.Result();
    /// </code></example>
    /// <para>Patterns may be nested arbitrarily as in the following example</para><example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///         .Case((IReferencer r) =&gt; r.ReferredTo
    ///             .Match().Yield&lt;double&gt;()
    ///                 .Case((Phrase p) =&gt; p.Words.OfNoun().Average(w =&gt; w.Weight))
    ///             .Result())
    ///         .Case((Noun n) =&gt; n.Weight)
    ///     .Result();
    /// </code></example>
    /// <para>
    /// When a Yield clause is not applied, the Match Expression will not yield a value. Instead it
    /// will behave much as a Type driven switch statement. <example>
    /// <code>
    /// myLexical.Match()
    ///         .Case((Phrase p) =&gt; Console.Write("Phrase: ", p.Text))
    /// 	    .Case((Word w) =&gt; Console.Write("Word: ", w.Text))
    ///     .Default(() =&gt; Console.Write("Not a Word or Phrase"));
    /// </code></example>
    /// </para>
    /// <para>
    /// * a: The visitor pattern provides statically type safe double dispatch, at the cost of
    ///   increased code complexity and increased class coupling.
    /// b: As LASI is implemented using C# 5.0, it has access to the language's built in support for
    ///    truly dynamic multi-methods Case arbitrary numbers of arguments. However while
    /// experimenting Case this approach, in a constrained scope and environment involving a fixed
    /// set of method overloads, this approach still had the drawbacks of reducing type safety,
    /// making extensions to type hierarchies potentially volatile, and drastically harming
    /// readability and maintainability.
    /// </para>
    /// <para>
    /// ** C# offers several methods of type checking, type casting, and type conversions, each Case
    ///    distinct semantics and sometimes drastically different performance characteristics. This
    /// is justification enough to form a centralized API and design pattern Casein the context of
    /// the project.(for example: if one algorithm is implemented using as/is operator semantics,
    /// which do not consider user defined conversions, it will not naturally adjust if the such
    /// conversions are defined)
    /// </para>
    /// </remarks>
    [System.Diagnostics.DebuggerStepThrough]
    public static class MatchExtensions
    {
        /// <summary>
        /// This externalized Case expression function allows for some slight additional flexibility.
        /// </summary>
        /// <typeparam name="TValue">The type of the value being matched over.</typeparam>
        /// <typeparam name="TCase">The type of the Case pattern.</typeparam>
        /// <typeparam name="TResult">The result type of the match expression.</typeparam>
        /// <typeparam name="TBaseResult">The result type of the pattern function.</typeparam>
        /// <param name="match">The match expression to which to append the Case clause.</param>
        /// <param name="func">The function which describes the case.</param>
        /// <returns>A match expression which now yields</returns>
        /// <remarks>
        /// This externalized Case expression function allows for some slight additional
        /// flexibility. Specifically it allows a <see cref="Match{T, TResult}"/> expression to be
        /// transformed into one where TResult is less derived in the case where a cause clause
        /// yields a result which is a of a base type of TResult. This will transform the match into
        /// a more general form which yields a TBase.
        /// </remarks>
        public static Match<TValue, TBaseResult> Case<TValue, TCase, TResult, TBaseResult>(
            this Match<TValue, TResult> match, Func<TCase, TBaseResult> func)
            where TValue : ILexical
            where TCase : ILexical
            where TResult : TBaseResult =>
            Match<TValue, TResult>.FromLowerToHigherResultType<TBaseResult, TResult>(match).Case(func);

        /// <summary>
        /// This externalized Case expression function allows for some slight additional flexibility.
        /// </summary>
        /// <typeparam name="TValue">The type of the value being matched over.</typeparam>
        /// <typeparam name="TCase">The type of the Case pattern.</typeparam>
        /// <typeparam name="TResult">The result type of the match expression.</typeparam>
        /// <typeparam name="TResultEnumerable">The result type of the pattern function.</typeparam>
        /// <param name="match">The match expression to which to append the Case clause.</param>
        /// <param name="func">The function which describes the case.</param>
        /// <returns>A match expression which now yields</returns>
        /// <remarks>
        /// This externalized Case expression function allows for some slight additional
        /// flexibility. Specifically it allows a Match&lt;T<see cref="System.Collections.Generic.IEnumerable{T}"/>&gt; expression to be
        /// transformed into one where element type of the resulting enumerable is less derived in the case where a cause clause
        /// yields a result which is a of a base type of TResult. This will transform the match into
        /// a more general form which yields a <see cref="IEnumerator{TBase}"/>.
        /// </remarks>
        public static Match<TValue, IEnumerable<TResult>> Case<TValue, TCase, TResultEnumerable, TResult>(
            this Match<TValue, TResultEnumerable> match, Func<TCase, IEnumerable<TResult>> func)
            where TResultEnumerable : IEnumerable<TResult>
            where TValue : class, ILexical
            where TCase : ILexical =>
            Match<TValue, IEnumerable<TResult>>.TransferValue(match)
                .Case<TValue, TCase, TResultEnumerable, IEnumerable<TResult>>(func);

        public static TBaseResult Result<TValue, TResult, TBaseResult>(this Match<TValue, TResult> match,
            TBaseResult defaultValue)
            where TResult : TBaseResult where TValue : ILexical =>
            Match<TValue, TResult>.FromLowerToHigherResultType<TBaseResult, TResult>(match).Result(defaultValue);

        public static TBaseResult Result<TValue, TResult, TBaseResult>(this Match<TValue, TResult> match,
            Func<TBaseResult> defaultValueFactory)
            where TResult : TBaseResult where TValue : ILexical =>
            Match<TValue, TResult>.FromLowerToHigherResultType<TBaseResult, TResult>(match).Result(defaultValueFactory);

        public static TBaseResult Result<TValue, TResult, TBaseResult>(this Match<TValue, TResult> match,
            Func<TValue, TBaseResult> defaultValueFactory)
            where TResult : TBaseResult where TValue : ILexical =>
            Match<TValue, TResult>.FromLowerToHigherResultType<TBaseResult, TResult>(match).Result(defaultValueFactory);

        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified
        /// ILexical value.
        /// </summary>
        /// <param name="value">The Lexical value to match against.</param>
        /// <returns>
        /// The head of a non result yielding Type based Pattern Matching expression over the
        /// specified ILexical value.
        /// </returns>
        public static Match<TValue> Match<TValue>(this TValue value)
            where TValue : ILexical => new Match<TValue>(value);

        /// <summary>
        /// Matches a value against a single case and immediately returns the result.
        /// </summary>
        /// <typeparam name="TValue">The type of the value being matched over.</typeparam>
        /// <typeparam name="TCase">The type of the Case pattern.</typeparam>
        /// <typeparam name="TResult">The result type of the match expression.</typeparam>
        /// <param name="value">The Lexical value to match against.</param>
        /// <param name="pattern">The single pattern case to try.</param>
        /// <returns>The result of matching the value against the specified pattern.</returns>
        public static TResult Match<TValue, TCase, TResult>(this TValue value, Func<TCase, TResult> pattern)
            where TValue : class, ILexical
            where TCase : ILexical => value.Match().Case(pattern).Result();

        /// <summary>
        /// Matches a value against a single case and immediately returns the result.
        /// </summary>
        /// <typeparam name="TValue">The type of the value being matched over.</typeparam>
        /// <typeparam name="TCase">The type of the Case pattern.</typeparam>
        /// <typeparam name="TResult">The result type of the match expression.</typeparam>
        /// <param name="value">The Lexical value to match against.</param>
        /// <param name="pattern">The single pattern case to try.</param>
        /// <returns>The result of matching the value against the specified pattern.</returns>
        /// <returns></returns>
        public static TResult Match<TValue, TCase, TResult>(this TValue value, Func<TValue, TResult> pattern)
            where TValue : class, ILexical
            where TCase : ILexical => value.Match().Case(pattern).Result();

        /// <summary>
        /// Matches a value against two case patterns and immediately returns the result.
        /// </summary>
        /// <typeparam name="TValue">The type of the value being matched over.</typeparam>
        /// <typeparam name="T1">The type of the first Case pattern.</typeparam>
        /// <typeparam name="T2">The type of the second Case pattern.</typeparam>
        /// <typeparam name="TResult">The result type of the match expression.</typeparam>
        /// <param name="value">The Lexical value to match against.</param>
        /// <param name="p1">The first pattern case to try.</param>
        /// <param name="p2">The second pattern case to try.</param>
        /// <returns>The result of matching the value against the two specified patterns.</returns>
        public static TResult Match<TValue, T1, T2, TResult>(this TValue value, Func<T1, TResult> p1,
            Func<T2, TResult> p2)
            where TValue : class, ILexical
            where T1 : class, ILexical
            where T2 : ILexical => value.Match().Case(p1).Case(p2).Result();

        /// <summary>
        /// Begins a pattern match expression over the <see cref="Sentence"/>.
        /// </summary>
        /// <param name="sentence">The sentence to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch Match(this Sentence sentence) => new SequenceMatch(sentence).AddLogger(Log);

        /// <summary>
        /// Begins a pattern match expression over the sequence of <see cref="ILexical"/>s.
        /// </summary>
        /// <param name="lexicalSequence">The sequence of to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch Match(this IEnumerable<ILexical> lexicalSequence) => new SequenceMatch(lexicalSequence).AddLogger(Log);

        /// <summary>
        /// Begins a pattern match expression over the sequence of <see cref="Phrase"/>s in the <see cref="Clause"/>.
        /// </summary>
        /// <param name="phrase">The clause to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch MatchSequence(this Phrase phrase) => new SequenceMatch(phrase.Words);

        public static IEnumerable<TResult> SelectCase<TLexical, TCase, TResult>(this IEnumerable<TLexical> lexicals,
            Func<TCase, TResult> caseSelector)
            where TLexical : ILexical
            where TCase : ILexical
        {
            return lexicals.SelectMany(enumerateWithCase);

            IEnumerable<TResult> enumerateWithCase(TLexical lexical)
            {
                if (lexical is TCase c)
                {
                    yield return caseSelector(c);
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TLexical, TResult>(this IEnumerable<TLexical> lexicals,
            Func<TLexical, Match<TLexical, TResult>> expression) where TLexical : ILexical =>
            SelectCase(lexicals, expression).Select(x => x.Result());
    }
}