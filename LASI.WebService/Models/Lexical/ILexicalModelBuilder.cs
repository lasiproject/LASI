using System;
using LASI.Core;

namespace LASI.WebService.Models.Lexical
{
    public interface ILexicalModelBuilder<in TLexical, out TModel> where TLexical :  ILexical where TModel :  ILexicalModel<TLexical>
    {
        TModel BuildFor(TLexical lexical);
    }
}