using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Core;
using LASI.WebApp.Old.Models.Lexical;
using LASI.Core.Heuristics;

namespace LASI.WebApp.Old
{
    public class LexicalModelBuilder : ILexicalModelBuilder<ILexical, ILexicalModel<ILexical>>
    {
        public ILexicalModel<ILexical> BuildFor<TFor>(ILexical lexical) where TFor : class, ILexical =>
            (from model in lexical.Match()
                .Case((Clause c) => CreateModel(c))
                .Case((Phrase p) => CreateModel(p))
                .Case((Word w) => CreateModel(w))
             select model).FirstOrDefault();

        private static ILexicalModel<ILexical> CreateModel(Clause c) => new ClauseModel(c);

        private static ILexicalModel<ILexical> CreateModel(Phrase p) => new PhraseModel(p);

        private static ILexicalModel<ILexical> CreateModel(Word w) => new WordModel(w);
    }
}