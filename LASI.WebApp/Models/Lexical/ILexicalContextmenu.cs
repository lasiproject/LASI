using System.Collections.Generic;

namespace LASI.WebApp.Models.Lexical
{
    public interface ILexicalContextmenu
    {
        int LexicalId { get; }
        IEnumerable<int> DirectObjectIds { get; }
        IEnumerable<int> IndirectObjectIds { get; }
        IEnumerable<int> SubjectIds { get; }
        IEnumerable<int> RefersToIds { get; }
    }
}