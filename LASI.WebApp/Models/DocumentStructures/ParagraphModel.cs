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
            SentenceModels = ModelFor.Sentences.Select(sentence => new SentenceModel(sentence));
            foreach (var model in SentenceModels) { model.ParagraphModel = this; }
            PhraseModels = ModelFor.Phrases.Select(phrase => new PhraseModel(phrase));
            foreach (var model in PhraseModels) { model.ParagraphModel = this; }
        }
        public PageModel PageModel { get; set; }
        [JsonIgnore]
        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "paragraph" };
        public IEnumerable<SentenceModel> SentenceModels { get; }
        public IEnumerable<PhraseModel> PhraseModels { get; }
        public override PageModel Parent => PageModel;

        public new string ContextmenuId { get; set; }
    }
}