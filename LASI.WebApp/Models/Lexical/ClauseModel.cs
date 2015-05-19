using LASI.WebApp.Models.DocumentStructures;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public class ClauseModel : LexicalModel<Clause>
    {
        public ClauseModel(Clause clause) : base(clause) { }
        public SentenceModel SentenceModel { get; internal set; }
        public override string ContextMenuJson { get; }
    }
}