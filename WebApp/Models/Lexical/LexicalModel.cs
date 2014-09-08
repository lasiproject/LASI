using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public abstract class LexicalModel
    {
        protected LexicalModel(ILexical element) {
            Element = element;
            Id = element.GetSerializationId();
            Text = element.Text;
            Style = SyntacticStyleMap[element];
        }
        public int Id { get; private set; }
        public string Text { get; private set; }
        public Style Style { get; private set; }
        public ILexical Element { get; private set; }
        protected static readonly SyntacticStyleMap SyntacticStyleMap = new SyntacticStyleMap();
    }
}