using System;
using System.Collections.Generic;
using System.Linq;
using AspSixApp.LexicalElementInfo;
using AspSixApp.Models.DocumentStructures;
using LASI.Core;
using LASI.Utilities;

namespace AspSixApp.Models.Lexical
{
    class PhraseModel : LexicalModel<Phrase>
    {
        public PhraseModel(Phrase phrase) : base(phrase) {
            ContextMenuJson = phrase.GetJsonMenuData();
            //Core.Phrase.VerboseOutput = true;
            DetailText = phrase.ToString().SplitRemoveEmpty('\n', '\r').Format(Tuple.Create(' ', ' ', ' '), s => s + "\n");
            WordViewModels = phrase.Words.Select(word => new WordModel(word));
            //foreach (var wvm in WordViewModels) {
            //wvm.PhraseModel = this;
            //}
        }
        public ParagraphModel ParagraphModel { get; set; }
        public override string ContextMenuJson { get; }
        public string DetailText { get; private set; }
        public IEnumerable<ILexicalModel<LASI.Core.Word>> WordViewModels { get; private set; }
        public SentenceModel SentenceModel { get; internal set; }
    }
}
