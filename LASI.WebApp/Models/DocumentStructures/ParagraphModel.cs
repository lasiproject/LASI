using System;
using System.Collections.Generic;
using System.Linq;
using LASI.WebApp.Models.Lexical;
using LASI.Core;
using Newtonsoft.Json;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class ParagraphModel : TextualModel<Paragraph, PageModel>
    {
        public ParagraphModel(Paragraph paragraph) : base(paragraph)
        {
            Sentences = ModelFor.Sentences.Select(sentence => new SentenceModel(sentence));
            foreach (var model in Sentences) { model.Paragraph = this; }
            Phrases = ModelFor.Phrases.Select(phrase => new PhraseModel(phrase));
            foreach (var model in Phrases) { model.Paragraph = this; }
        }
        public PageModel Page { get; set; }
        [JsonIgnore]
        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "paragraph" };
        public IEnumerable<SentenceModel> Sentences { get; }
        public IEnumerable<PhraseModel> Phrases { get; }
        public override PageModel Parent => Page;

        public new string ContextmenuId { get; set; }
    }
}