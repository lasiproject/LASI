using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Core;
using LASI.WebApp.Models.Lexical;
using LASI.Core.Heuristics;

namespace LASI.WebApp
{
    public class LexicalModelBuilder : ILexicalModelBuilder<ILexical, ILexicalModel<ILexical>>
    {
        public ILexicalModel<ILexical> BuildFor<TFor>(ILexical lexical) where TFor : class, ILexical
        {
            return lexical.Match()
                    .Case((Clause c) => CreateModel(c))
                    .Case((Phrase p) => CreateModel(p))
                    .Case((Word w) => CreateModel(w))
            .Result();
        }

        private static ILexicalModel<ILexical> CreateModel(Clause c) => new ClauseModel(c);

        private static ILexicalModel<ILexical> CreateModel(Phrase p) => new PhraseModel(p);

        private static ILexicalModel<ILexical> CreateModel(Word w) => new WordModel(w);
    }
}