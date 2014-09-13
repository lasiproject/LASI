using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    interface ILexicalModel<TLexical> where TLexical :ILexical
    {
        TLexical Element { get; }
        int Id { get; }
        Style Style { get; }
        string Text { get; }
    }
}