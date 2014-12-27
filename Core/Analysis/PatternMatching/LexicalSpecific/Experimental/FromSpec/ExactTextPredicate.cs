using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.FromSpec
{
    class ExactTextPredicate<T> : Predicate<T> where T : class, ILexical
    {
        private readonly string text;
        /// <summary>
        /// Intializes a new instance of the <see cref="ExactTextPredicate"/> class.
        /// </summary>
        /// <param name="text">
        /// The text which an element must exactly match to satisfy the predicate. 
        /// </param>
        public ExactTextPredicate(string text) { this.text = text; }
        /// <summary>
        /// Returns a value indicating if the given <see cref="ILexical"> instance's textual content
        /// exactly matches the value from which the predicate was constructed<see cref="Predicate">.
        /// </summary>
        /// <param name="element"> The <see cref="ILexical"/> instance to test.</param>
        /// <returns>
        /// <c>true</c> if the given element satisfies the predicate; otherwise <c>false</c>.
        /// </returns>
        public override bool Satifies<TLexical>(TLexical element) => element.Text == text;
        public static Predicate<T> operator &(Predicate<T> left, ExactTextPredicate<T> right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));
        public static Predicate<T> operator &(CaseTypePredicate<T, ILexical> left, ExactTextPredicate<T> right) => new WhenPredicate<T>(x => left.Combine(right).Satifies(x));
        public static implicit operator ExactTextPredicate<T>(string text) => new ExactTextPredicate<T>(text);
    }
}
