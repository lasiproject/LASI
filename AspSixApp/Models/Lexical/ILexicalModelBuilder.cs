using System;
using LASI.Core;

namespace AspSixApp.Models.Lexical
{
    public interface ILexicalModelBuilder<in TLexical, out TModel> where TLexical : class, ILexical where TModel : class, ILexicalModel<TLexical>
    {
        TModel BuildFor(TLexical lexical);
    }
}