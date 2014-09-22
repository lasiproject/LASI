using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Interop
{
    /// <summary>
    /// Represents a syntactic mapping from Lexical element Types to stylization. 
    /// </summary>
    /// <typeparam name="TLexical">The Type of Lexical elements to map from. This type parameter is contravariant. That is,
    /// you can use either the type you specified or any type that is less derived.</typeparam>
    /// <typeparam name="TStylization">The Type of stylization values to map to. This type parameter is covariant. That is,
    /// you can use either the type you specified or any type that is more derived.</typeparam>
    public interface IStyleProvider<in TLexical, out TStylization> where TLexical : ILexical
    {
        /// <summary>
        /// Gets a stylization value corresponding to the syntactic nature of the indexing lexical element. 
        /// Implementors should provide a default value as a fall back.
        /// </summary>
        /// <param name="syntacticElement">The lexical element to map to a stylization value.</param>
        /// <returns>A stylization value corresponding to the syntactic nature of the indexing lexical element.</returns>
        TStylization this[TLexical syntacticElement] { get; }
    }
}
