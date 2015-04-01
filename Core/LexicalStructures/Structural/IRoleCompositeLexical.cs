using System.Collections.Generic;

namespace LASI.Core.LexicalStructures.Structural
{
    public interface IRoleCompositeLexical<out TUnit, out TRole>
        where TUnit : IUnitLexical, ILexical
        where TRole : TUnit, ILexical
    {
        IEnumerable<TRole> RoleWords { get; }
    }
}