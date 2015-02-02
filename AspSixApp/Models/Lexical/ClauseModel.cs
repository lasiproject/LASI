using AspSixApp.Models.DocumentStructures;
using LASI.Core;

namespace AspSixApp.Models.Lexical
{
    class ClauseModel : LexicalModel<Clause>
	{
		public ClauseModel(Clause clause) : base(clause) { }

		public SentenceModel SentenceModel { get; internal set; }
		public override string ContextMenuJson { get; }
	}
}