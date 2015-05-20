using System;
using System.Collections.Generic;
using System.Linq;
using LASI.WebApp.LexicalElementInfo;
using LASI.WebApp.Models.DocumentStructures;
using LASI.Core;
using LASI.Utilities;

namespace LASI.WebApp.Models.Lexical
{
    public class PhraseModel : LexicalModel<Phrase>
    {
        public PhraseModel(Phrase phrase) : base(phrase)
        {
            ContextMenuJson = ModelFor.GetJsonMenuData();
            DetailText = ModelFor.ToString().SplitRemoveEmpty('\n', '\r').Format(Tuple.Create(' ', ' ', ' '), s => s + "\n");
            Words = from word in ModelFor.Words
                    select new WordModel(word);
        }
        public ParagraphModel Paragraph { get; set; }
        public override string ContextMenuJson { get; }
        public string DetailText { get; }
        public IEnumerable<ILexicalModel<Word>> Words { get; }
        public SentenceModel Sentence { get; internal set; }
    }
}
