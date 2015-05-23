using System;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    class WordModel : LexicalModel<Word>
    {
        public WordModel(Word word) : base(word) { }
        public PhraseModel Phrase { get; set; }
        public override ILexicalContextmenu Contextmenu { get; }
        public override string DetailText => ModelFor.ToString();
    }
}