using System;
using LASI.WebApp.LexicalElementInfo;
using LASI.WebApp.LexicalElementStyling;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public abstract class LexicalModel<TLexical> : ILexicalModel<TLexical>, ILinkedViewModel<TLexical> where TLexical : class, ILexical
    {
        protected LexicalModel(TLexical element)
        {
            Element = element;
            Id = element.GetSerializationId();
            Text = element.Text;
            Style = SyntacticStyleMap[element];
        }
        public int Id { get; }
        public string Text { get; }
        public Style Style { get; }
        [Newtonsoft.Json.JsonIgnore]
        public TLexical Element { get; }
        [Newtonsoft.Json.JsonIgnore]
        public TLexical ModelFor => Element;
        public abstract string ContextMenuJson { get; }

        public string ContextmenuId { get; }

        protected static readonly SyntacticStyleMap SyntacticStyleMap = new SyntacticStyleMap();
    }
}