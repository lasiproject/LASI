using System;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public interface ILexicalModelBuilder<in TLexical, out TModel> where TLexical : class, ILexical where TModel : class, ILexicalModel<TLexical>
    {
        TModel BuildFor(TLexical lexical);
    }
}