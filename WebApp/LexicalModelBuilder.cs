using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Core;
using LASI.WebApp.Models.Lexical;
using LASI.Core.Heuristics;

namespace LASI.WebApp
{
	class LexicalModelBuilder : ILexicalModelBuilder<ILexical, ILexicalModel<ILexical>>
	{
		public ILexicalModel<ILexical> BuildFor<TFor>(ILexical lexical) where TFor : class, ILexical {
			return lexical.Match()
				.Yield<ILexicalModel<ILexical>>()
					.Case((Clause c) => CreateModel(c))
					.Case((Phrase p) => CreateModel(p))
					.Case((Word w) => CreateModel(w))
			.Result();
		}

		private ILexicalModel<Clause> CreateModel(Clause c) {
			throw new NotImplementedException();
		}

		private ILexicalModel<Phrase> CreateModel(Phrase p) {
			throw new NotImplementedException();
		}

		private ILexicalModel<Word> CreateModel(Word w) {
			throw new NotImplementedException();
		}
	}
}