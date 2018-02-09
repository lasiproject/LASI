using System;
using System.Collections.Generic;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Provides access to predefined and customizable IEqualityComparer implementations which operate on instances of applicable ILexical types.
    /// </summary>
    /// <seealso cref="ILexical"/>
    public static class LexicalComparers
    {
        /// <summary>
        /// Gets a IEqualityComparer&lt;ILexical&gt; which uses a default, case-sensitive textual matching function.
        /// </summary>
        public static IEqualityComparer<ILexical> Textual => Equality.Create<ILexical>((x, y) => x.Text == y.Text, x => x.Text.GetHashCode());
    }
}
