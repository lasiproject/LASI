using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.WebApp.Models.Lexical
{
	class PhraseModel : LexicalModel<Core.Phrase>
	{
		public PhraseModel(Core.Phrase phrase) : base(phrase) {
			ContextMenuJson = phrase.GetJsonMenuData();
			//Core.Phrase.VerboseOutput = true;
			DetailText = phrase.ToString().SplitRemoveEmpty('\n', '\r').Format(Tuple.Create(' ', ' ', ' '), s => s + "\n");
			WordViewModels = phrase.Words.Select(word => new WordModel(word));
			foreach (var wvm in WordViewModels) {
				//wvm.PhraseModel = this;
			}
		}
		public ParagraphModel ParagraphModel { get; set; }
		public override string ContextMenuJson { get; }
		public string DetailText { get; private set; }
		public IEnumerable<ILexicalModel<Core.Word>> WordViewModels { get; private set; }
		public SentenceModel SentenceModel { get; internal set; }
	}
}
