using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace LASI.Core
{
    /// <summary>
    /// Provides for the construction of flexible Typed Pattern Matching expressions.
    /// </summary>
    /// <see cref="LASI.Core.PatternMatching.Match{T}"/>
    /// <seealso cref="LASI.Core.PatternMatching.Match{T, TResult}"/>
    /// <seealso cref="LASI.Core.PatternMatching"/>
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
    public static class PatternMatcher
    {
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified ILexical value.
        /// </summary> 
        /// <param name="value">The Lexical value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified ILexical value.</returns>
        public static LASI.Core.PatternMatching.Match<T> Match<T>(this T value) where T : class, ILexical {
            return new LASI.Core.PatternMatching.Match<T>(value);
        }
        static void Test(ILexical l) {
            double x = l
            | new Case { (ILexical v) => v.Weight }
            | new Case { };

        }
    }
    public static class Pattern
    {
    }
    public class Pattern<TResult> : System.Collections.IEnumerable
    {

        private bool accepted = false;
        private List<Func<ILexical, TResult>> patterns = new List<Func<ILexical, TResult>>();
        private ILexical value; TResult result = default(TResult);

        public TResult Result {
            get {
                return result;
            }
        }

        private void StorePattern<TPattern>(Func<TPattern, TResult> f)
            where TPattern : class, ILexical { patterns.Add(x => { var y = value as TPattern; accepted = y != null; return (y != null) ? f(y) : default(TResult); }); }
        private void Apply(ILexical value) {
            this.value = value;
            for (var i = 0; !accepted && i < patterns.Count; i++) {
                result = patterns[i](value);
            }
        }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IAggregateEntity, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IEntity, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IReferencer, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IDescriptor, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IAdverbial, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IConjunctive, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<IPrepositional, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Adverb, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<ProperSingularNoun, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<ProperPluralNoun, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<NounPhrase, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<AdjectivePhrase, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<PronounPhrase, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<VerbPhrase, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<InfinitivePhrase, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Adjective, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Preposition, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Conjunction, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<CommonNoun, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<ProperNoun, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Noun, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Pronoun, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<ILexical, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Word, TResult> f) { p.StorePattern(f); return p; }
        public static Pattern<TResult> operator |(Pattern<TResult> p, Func<Phrase, TResult> f) { p.StorePattern(f); return p; }


        public void Add(Func<IVerbal, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IAggregateEntity, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IEntity, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IReferencer, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IDescriptor, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IAdverbial, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IConjunctive, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<IPrepositional, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Adverb, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<ProperSingularNoun, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<ProperPluralNoun, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<NounPhrase, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<PronounPhrase, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<VerbPhrase, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<InfinitivePhrase, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Adjective, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Preposition, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Conjunction, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<CommonNoun, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<ProperNoun, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Noun, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Pronoun, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<ILexical, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Word, TResult> f) {
            StorePattern(f);
        }
        public void Add(Func<Phrase, TResult> f) {
            StorePattern(f);
        }
        //public static TResult operator |(ILexical value, Pattern<TResult> pattern) {
        //    pattern.Apply(value); return pattern.result;
        //}


        //public static implicit operator TResult(Pattern<TResult> pattern) {
        //    return pattern.result;
        //}
        ////public static TResult operator |(Pattern<TResult> pattern, Func<IEntity, TResult> f) {
        //    var x = default(IEntity)
        //        | new Case { (IEntity y) => f(y) }
        //        | new Case { (IEntity y) => default(TResult) }
        //        | new Case { (IEntity e) => default(TResult) };



        //    return new Pattern<TResult> { };
        //}
        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

    }
    public class Case : System.Collections.IEnumerable
    {
        //public Pattern<TResult> Add(Func<IEntity, TResult> f) {
        //    return new Pattern<TResult> { };
        //}
        public static implicit operator Case(Func<IAggregateEntity, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IVerbal, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IEntity, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IReferencer, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IDescriptor, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IAdverbial, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IConjunctive, object> f) { return new Case { }; }
        public static implicit operator Case(Func<IPrepositional, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Adverb, object> f) { return new Case { }; }
        public static implicit operator Case(Func<ProperSingularNoun, object> f) { return new Case { }; }
        public static implicit operator Case(Func<ProperPluralNoun, object> f) { return new Case { }; }
        public static implicit operator Case(Func<NounPhrase, object> f) { return new Case { }; }
        public static implicit operator Case(Func<AdjectivePhrase, object> f) { return new Case { }; }
        public static implicit operator Case(Func<PronounPhrase, object> f) { return new Case { }; }
        public static implicit operator Case(Func<VerbPhrase, object> f) { return new Case { }; }
        public static implicit operator Case(Func<InfinitivePhrase, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Adjective, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Preposition, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Conjunction, object> f) { return new Case { }; }
        public static implicit operator Case(Func<CommonNoun, object> f) { return new Case { }; }
        public static implicit operator Case(Func<ProperNoun, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Noun, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Pronoun, object> f) { return new Case { }; }
        public static implicit operator Case(Func<ILexical, object> f) { return new Case { }; }
        public static implicit operator Case(Func<Word, object> f) { return new Case { }; }
        public static Case operator |(Case p, Func<IAggregateEntity, object> f) { return p; }
        public static Case operator |(Case p, Func<IVerbal, object> f) { return p; }
        public static Case operator |(Case p, Func<IEntity, object> f) { return p; }
        public static Case operator |(Case p, Func<IReferencer, object> f) { return p; }
        public static Case operator |(Case p, Func<IDescriptor, object> f) { return p; }
        public static Case operator |(Case p, Func<IAdverbial, object> f) { return p; }
        public static Case operator |(Case p, Func<IConjunctive, object> f) { return p; }
        public static Case operator |(Case p, Func<IPrepositional, object> f) { return p; }
        public static Case operator |(Case p, Func<Adverb, object> f) { return p; }
        public static Case operator |(Case p, Func<ProperSingularNoun, object> f) { return p; }
        public static Case operator |(Case p, Func<ProperPluralNoun, object> f) { return p; }
        public static Case operator |(Case p, Func<NounPhrase, object> f) { return p; }
        public static Case operator |(Case p, Func<AdjectivePhrase, object> f) { return p; }
        public static Case operator |(Case p, Func<PronounPhrase, object> f) { return p; }
        public static Case operator |(Case p, Func<VerbPhrase, object> f) { return p; }
        public static Case operator |(Case p, Func<InfinitivePhrase, object> f) { return p; }
        public static Case operator |(Case p, Func<Adjective, object> f) { return p; }
        public static Case operator |(Case p, Func<Preposition, object> f) { return p; }
        public static Case operator |(Case p, Func<Conjunction, object> f) { return p; }
        public static Case operator |(Case p, Func<CommonNoun, object> f) { return p; }
        public static Case operator |(Case p, Func<ProperNoun, object> f) { return p; }
        public static Case operator |(Case p, Func<Noun, object> f) { return p; }
        public static Case operator |(Case p, Func<Pronoun, object> f) { return p; }
        public static Case operator |(Case p, Func<ILexical, object> f) { return p; }
        public static Case operator |(Case p, Func<Word, object> f) { return p; }
        public static implicit operator double (Case c) { return 0; }

        public Case Add(Func<Phrase, object> f) { return this; }
        public Case Add(Func<IAggregateEntity, object> f) { return this; }
        public Case Add(Func<IEntity, object> f) { return this; }
        public Case Add(Func<IVerbal, object> f) { return this; }
        public Case Add(Func<IReferencer, object> f) { return this; }
        public Case Add(Func<IDescriptor, object> f) { return this; }
        public Case Add(Func<IAdverbial, object> f) { return this; }
        public Case Add(Func<IConjunctive, object> f) { return this; }
        public Case Add(Func<IPrepositional, object> f) { return this; }
        public Case Add(Func<Adverb, object> f) { return this; }
        public Case Add(Func<ProperSingularNoun, object> f) { return this; }
        public Case Add(Func<ProperPluralNoun, object> f) { return this; }
        public Case Add(Func<NounPhrase, object> f) { return this; }
        public Case Add(Func<AdjectivePhrase, object> f) { return this; }
        public Case Add(Func<PronounPhrase, object> f) { return this; }
        public Case Add(Func<VerbPhrase, object> f) { return this; }
        public Case Add(Func<InfinitivePhrase, object> f) { return this; }
        public Case Add(Func<Adjective, object> f) { return this; }
        public Case Add(Func<Preposition, object> f) { return this; }
        public Case Add(Func<Conjunction, object> f) { return this; }
        public Case Add(Func<CommonNoun, object> f) { return this; }
        public Case Add(Func<ProperNoun, object> f) { return this; }
        public Case Add(Func<Noun, object> f) { return this; }
        public Case Add(Func<Pronoun, object> f) { return this; }
        public Case Add(Func<ILexical, object> f) { return this; }
        public Case Add(Func<Word, object> f) { return this; }
        public Case Add(Case p, Func<Phrase, object> f) { return this; }


        //public Case(Func<Phrase, object> f) { }
        //public Case(Func<IAggregateEntity, object> f) { }
        //public Case(Func<IEntity, object> f) { }
        //public Case(Func<IReferencer, object> f) { }
        //public Case(Func<IDescriptor, object> f) { }
        //public Case(Func<IAdverbial, object> f) { }
        //public Case(Func<IConjunctive, object> f) { }
        //public Case(Func<IPrepositional, object> f) { }
        //public Case(Func<Adverb, object> f) { }
        //public Case(Func<ProperSingularNoun, object> f) { }
        //public Case(Func<ProperPluralNoun, object> f) { }
        //public Case(Func<NounPhrase, object> f) { }
        //public Case(Func<AdjectivePhrase, object> f) { }
        //public Case(Func<PronounPhrase, object> f) { }
        //public Case(Func<VerbPhrase, object> f) { }
        //public Case(Func<InfinitivePhrase, object> f) { }
        //public Case(Func<Adjective, object> f) { }
        //public Case(Func<Preposition, object> f) { }
        //public Case(Func<Conjunction, object> f) { }
        //public Case(Func<CommonNoun, object> f) { }
        //public Case(Func<ProperNoun, object> f) { }
        //public Case(Func<Noun, object> f) { }
        //public Case(Func<Pronoun, object> f) { }
        //public Case(Func<ILexical, object> f) { }
        //public Case(Func<Word, object> f) { }
        //public Case(Case p, Func<Phrase, object> f) { }

        //public static Case operator |(Case p, Func<IEntity, TResult> f) {
        //    return new Case { };
        //}
        public static Case operator |(Case p, Case f) {
            return new Case { };
        }
        public static Case operator |(ILexical p, Case f) {
            return new Case { };
        }
        public IEnumerator GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
