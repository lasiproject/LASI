using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.ViewModels
{
    public abstract class LexicalViewModel
    {
        protected LexicalViewModel(ILexical element) {
            this.element = element;
            Id = element.GetSerializationId();
            Text = element.Text;
            Style = SyntacticStyleMap[element];
        }
        public int Id { get; private set; }
        public string Text { get; private set; }
        public Style Style { get; private set; }
        protected static readonly SyntacticStyleMap SyntacticStyleMap = new SyntacticStyleMap();
        private ILexical element;
    }
}