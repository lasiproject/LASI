using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using LASI.Core.PatternMatching;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;

namespace LASI.Core
{
    /// <summary>
    /// Provides for the construction of flexible Typed Pattern Matching expressions.
    /// </summary>
    /// <see cref="PatternMatching.Match{T}"/>
    /// <see cref="PatternMatching.Match{T, TResult}"/>
    /// <see cref="PatternMatching"/>
    ///<remarks>
    /// <para>
    /// The type based pattern matching functionality was introduced to solve the problem of performing subtype dependent operations which could not be described by traditional virtual method approaches in an expressive or practical manner.
    /// There are several reasons for this. First and most important, is that the virtual method approach is limited to single dispatch* . Secondly different binding operations must be chosen based on a variety of information gathered from contexts 
    /// which are often external to the single instance of the type representing a lexical construct. Different numbers and types of arguments that depend on the surrounding context of the lexical instnace cannot be specified in a manner compatible 
    /// Case virtual method signatures. However, a linear storage and traversal mechanism for lexical elements of differing syntactic types, as well as the need to define types representing elements having overlapping syntactic roles,
    /// remains a necessity which prevents the complete stratification of class and interface hierarchies.
    /// There were a variety of solutions that were considered:
    /// </para>
    /// <para>
    /// 1. A visitor pattern could be implemented such that the an object traversing a sequence of lexical elements would be passed in turn to each, and by carrying Case it sufficient and arbitrary context 
    ///     and by providing an overload for every syntactic type, could provide functionality to implement operations between elements. There are several drawbacks involved including increased state disperal, increased class coupling,
    ///     the maintenance cost of re factoring types, the need to define new classes, which carry arbitrary algorithm state Case them but must exist in their own hierarchy, and other factors generally increasing complexity.
    /// </para>
    /// <para>
    /// 2. A more flexible, visitor-like pattern could be implemented by taking advantage of dynamic dispatch and runtime overload resolution.
    ///     This has the drawback of the code not clearly expressing its semantics statically as it essentially relies on the runtime to describe control flow, never stating it explicitly.
    /// </para>
    /// <para>
    /// 3. Describe traversal algorithms using complex, nested tiers of conditional blocks determined by the results of type casts. This is error prone, unwieldy, ugly, and obscures the logic Case noise. Additionally this approach may be inconsistently applied in different section of code, causing the implementations of algorithms so written to be prone to subtle errors**.  
    /// </para><para>
    /// The pattern matching implemented here abstracts over the type checking logic, allowing it to be tested and optimized for the large variety of algorithms in need of such functionality, 
    /// allows for subtype constraints to be specified, allows for algorithms to handle as many or as few types as needed, and emphasizes the intent of the code which leverage it. 
    /// It also eliminates the difficulty of defining state-full visitors by defining a match Case a particular subtype as a function on that type. Such a function is intuitively specified as an anonymous closure which
    /// implicitly captures any state it will use. This approach also encourages the localization of logic, which in the case of visitors would not possible as implementations of visitors will inherently get spread out in both the textual space of the source code and the conceptions of implementers.
    /// The syntax for pattern matching uses a fluent interface style.
    /// </para>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///         .Case((IReferencer r) => r.ReferredTo.Weight)
    ///     	.Case((IEntity e) => e.Weight)
    ///     	.Case((IVerbal v) => v.HasSubject()? v.Subject.Weight : 0)
    ///		.Result(1);	
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///			.Case((Phrase p) => p.Words.Average(w => w.Weight))
    ///			.Case((Word w) => w.Weight)
    ///		.Result();
    /// </code>
    /// </example>
    /// <para>
    /// Patterns may be nested arbitrarily as in the following example
    /// </para>
    /// <example>
    /// <code>
    /// var weight = myLexical.Match().Yield&lt;double&gt;()
    ///         .Case((IReferencer r) => r.ReferredTo
    ///             .Match().Yield&lt;double&gt;()
    ///                 .Case((Phrase p) => p.Words.OfNoun().Average(w => w.Weight))
    ///             .Result())
    ///         .Case((Noun n) => n.Weight)
    ///     .Result();
    /// </code>
    /// </example>
    /// <para>
    /// When a Yield clause is not applied, the Match Expression will not yield a value. Instead it will behave much as a Type driven switch statement. 
    /// <example>
    /// <code>
    /// myLexical.Match()
    ///         .Case((Phrase p) => Console.Write(&quot;Phrase: &quot;, p.Text))
    ///		    .Case((Word w) => Console.Write(&quot;Word: &quot;, w.Text))
    ///	    .Default(() => Console.Write(&quot;Not a Word or Phrase&quot;));
    /// </code>
    /// </example>
    /// </para>
    /// <para>
    /// * a: The visitor pattern provides statically type safe double dispatch, at the cost of increased code complexity and increased class coupling.
    /// b: As LASI is implemented using C# 5.0, it has access to the language's built in support for truly dynamic multi-methods Case arbitrary numbers of arguments.
    /// However while experimenting Case this approach, in a constrained scope and environment involving a fixed set of method overloads, this approach still had the drawbacks
    ///	 of reducing type safety, making extensions to type hierarchies potentially volatile, and drastically harming readability and maintainability.
    ///	 </para>
    /// <para>
    ///	 ** C# offers several  methods of type checking, type casting, and type conversions, each Case distinct semantics and sometimes drastically different performance characteristics.
    ///	This is justification enough to form a centralized API and design pattern Casein the context of the project.(for example: if one algorithm is implemented using 
    ///	as/is operator semantics, which do not consider user defined conversions, it will not naturally adjust if the such conversions are defined)
    /// </para>
    /// </remarks>
    public static class Matcher
    {
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified ILexical value.
        /// </summary> 
        /// <param name="value">The Lexical value to match Case.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified ILexical value.</returns>
        public static Match<T> Match<T>(this T value) where T : class, ILexical {
            return new Match<T>(value);
        }
        public static SequenceMatch Match(this Sentence sentence) {
            return new SequenceMatch(sentence);
        }
        public static SequenceMatch Match(this IEnumerable<Word> words) {
            return new SequenceMatch(words);
        }
        public static SequenceMatch Match(this IEnumerable<Phrase> phrases) {
            return new SequenceMatch(phrases);
        }
    }
}
