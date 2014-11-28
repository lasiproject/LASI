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
        public ILexicalModel<ILexical> BuildFor(ILexical lexical) {
            return lexical.Match().Yield<ILexicalModel<ILexical>>()
                .With((Clause c) => new ClauseModel(c))
                .With((Phrase p) => new PhraseModel(p))
                .With((Word w) => new WordModel(w))
            .Result();
        }
    }
}