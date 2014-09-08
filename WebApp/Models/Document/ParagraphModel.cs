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
            SentenceModels = paragraph.Sentences.Select(sentence => new SentenceModel(sentence));
            foreach (var model in SentenceModels) { model.ParagraphModel = this; }
            PhraseModels = paragraph.Phrases.Select(phrase => new PhraseModel(phrase));
            foreach (var model in PhraseModels) { model.ParagraphModel = this; }
        }
        public PageModel PageModel { get; set; }
        public string Text { get; private set; }
        public Style Style { get; private set; }
        public IEnumerable<SentenceModel> SentenceModels { get; private set; }
        public IEnumerable<PhraseModel> PhraseModels { get; private set; }
    }
}