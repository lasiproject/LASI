using System;
using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Old.Models.Lexical
{
    public abstract class LexicalModel<TLexical> : ILexicalModel<TLexical>, IViewModel<TLexical> where TLexical : class, ILexical
    {
        protected LexicalModel(TLexical element) {
            Element = element;
            Id = element.GetSerializationId();
            Text = element.Text;
            Style = SyntacticStyleMap[element];
        }
        public int Id { get; }
        public string Text { get; }
        public Style Style { get; }
        public TLexical Element { get; }

        public TLexical ModelFor => Element;

        public abstract string ContextMenuJson { get; }

        protected static readonly SyntacticStyleMap SyntacticStyleMap = new SyntacticStyleMap();
    }
}