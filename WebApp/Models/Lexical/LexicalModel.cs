using System;
using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public abstract class LexicalModel<TLexical> : ILexicalModel<TLexical>, IViewModel<TLexical> where TLexical : class, ILexical
    {
        protected LexicalModel(TLexical element) {
            Element = element;
            Id = element.GetSerializationId();
            Text = element.Text;
            Style = SyntacticStyleMap[element];
        }
        public int Id { get; private set; }
        public string Text { get; private set; }
        public Style Style { get; private set; }
        public TLexical Element { get; private set; }

        public TLexical ModelFor { get { return Element; } }

        protected static readonly SyntacticStyleMap SyntacticStyleMap = new SyntacticStyleMap();
    }
}