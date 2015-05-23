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
            Phrases = ModelFor.Phrases.Select(phrase => new PhraseModel(phrase));
        }
        [JsonIgnore]
        public PageModel Page { get; set; }
        [JsonIgnore]
        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "paragraph" };
        public IEnumerable<SentenceModel> Sentences { get; }
        [JsonIgnore]
        public IEnumerable<PhraseModel> Phrases { get; }
        [JsonIgnore]
        public override PageModel Parent => Page;
    }
}