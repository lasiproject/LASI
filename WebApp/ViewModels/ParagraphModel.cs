using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LASI.Core.DocumentStructures;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
    public class ParagraphModel
    {
        public ParagraphModel(Paragraph paragraph) {
            Text = paragraph.Text;
            Style = new Style { CssClass = "paragraph" };
            SentenceViewModels = paragraph.Sentences.Select(sentence => new SentenceModel(sentence));
            PhraseViewModels = paragraph.Phrases.Select(phrase => new PhraseModel(phrase));
        }
        public string Text { get; private set; }
        public Style Style { get; private set; }
        public IEnumerable<SentenceModel> SentenceViewModels { get; private set; }
        public IEnumerable<PhraseModel> PhraseViewModels { get; private set; }
    }
}