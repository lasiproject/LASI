using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public interface ILexicalModel<out TLexical> where TLexical :  ILexical
    {
        [Newtonsoft.Json.JsonIgnore]
        TLexical Element { get; }
        string Text { get; }
        string DetailText { get; }
        Style Style { get; }
        int Id { get; }
        ILexicalContextmenu Contextmenu { get; }
    }
}