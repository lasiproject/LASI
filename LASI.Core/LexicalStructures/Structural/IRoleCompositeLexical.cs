using System.Collections.Generic;

namespace LASI.Core.LexicalStructures.Structural
{
    /// <summary>
    /// Represents a composite lexical which has a number of components which define a single syntactic role.
    /// </summary>
    /// <typeparam name="TUnit">The type of the elements of the composite lexical.</typeparam>
    /// <typeparam name="TRole">The type of the syntactic role of the composite lexical.</typeparam>
    public interface IRoleCompositeLexical<out TUnit, out TRole>
        where TUnit : IUnitLexical, ILexical
        where TRole : TUnit, ILexical
    {
        /// <summary>Gets the components which comprise the role of IRoleCompositeLexical&lt;<typeparamref name="TUnit"/>, <typeparamref name="TRole"/>&gt;.</summary>
        IEnumerable<TRole> RoleComponents { get; }
    }
}