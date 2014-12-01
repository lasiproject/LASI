using LASI;
using LASI.Core.Heuristics;
using LASI.Core.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Contracts.Validators;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Provides access to predefined and customizable IEqualityComparer implementations which operate on instances of applicable ILexical types.
    /// </summary>
    /// <see cref="ILexical"/>
    public static class LexicalComparers
    {
        /// <summary>
        /// Creates a custom IEqualityComparer which always uses the provided comparison function to compare instances. 
        /// This fully bypasses the hash checks for non null references.
        /// </summary>
        /// <param name="equals">The function to determine equality.</param>
        /// <returns>
        /// A custom NounPhraes comparer which uses the given function to compare values and a null or not null only sensitive Hash function.
        /// </returns>
        /// <remarks>The intent of the functionality provided is to simplify
        /// the use of LINQ methods, such as IEnumerable.Contains(item) and IEnumerable.Distinct() 
        /// which require an IEqualityComparer as opposed to a simple predicate function.
        /// Because the custom comparer created bypasses hash code equality assumptions, it allows for these methods to behave more transparently.
        /// This however means that the overhead of using the returned comparer in a LINQ query is substantial. 
        /// Specifically, the complexity of a calling IEnumerable.Contains(CreateCustom(Func)) or IEnumerable.Distinct(CreateCustom(Func))
        /// approaches O(N^2), where as calls which use the default reference based, hash if possible comparers, IEqualityComparers only approach approach O(N).
        /// </remarks>
        public static IEqualityComparer<TLexical> Create<TLexical>(Func<TLexical, TLexical, bool> equals) where TLexical : ILexical {
            ArgumentValidator.ThrowIfNull(equals, "equals", "A null equals function was provided.");
            return new CustomComparer<TLexical>(equals);
        }
        /// <summary>
        /// Gets a IEqualityComparer&lt;ILexical&gt; which uses a default, case-sensitive textual matching function.
        /// </summary>
        public static IEqualityComparer<ILexical> Textual {
            get { return new CustomComparer<ILexical>((l1, l2) => l1.Text == l2.Text, l => l.Text.GetHashCode()); }
        }
    }
}
