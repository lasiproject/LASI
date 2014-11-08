using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    interface ILexicalModel<out TLexical> where TLexical : class, ILexical
    {
        TLexical Element { get; }
        string Text { get; }
    }
}