using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public interface ILexicalModel<out TLexical> where TLexical : class, ILexical
    {
        TLexical Element { get; }
        string Text { get; }
        Style Style { get; }
        int Id { get; }
        string ContextMenuJson { get; }
    }
}