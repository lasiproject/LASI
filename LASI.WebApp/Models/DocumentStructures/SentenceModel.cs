using System;
using System.Collections.Generic;
using System.Linq;
using LASI.WebApp.Models.Lexical;
using LASI.Core;
using LASI.Utilities;
using Newtonsoft.Json;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class SentenceModel : TextualModel<Sentence, ParagraphModel>
    {
        public SentenceModel(Sentence sentence) : base(sentence)
        {
            Phrases = ModelFor.Phrases
               .Select(phrase => new PhraseModel(phrase))
               .Append(new PhraseModel(new SymbolPhrase(sentence.Ending)));
            Phrases.ForEach(m => m.Sentence = this);
            #region
            //ClauseModels = sentence.Clauses.Select(clause => new ClauseModel(clause));
            //foreach (var model in ClauseModels)
            //{
            //    model.SentenceModel = this;
            //}
            #endregion
        }
        [JsonIgnore]
        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "sentence" };
        public IEnumerable<PhraseModel> Phrases { get; }

        //public IEnumerable<ClauseModel> ClauseModels { get; }

        public ParagraphModel Paragraph { get; internal set; }
        public override ParagraphModel Parent => Paragraph;
    }
}