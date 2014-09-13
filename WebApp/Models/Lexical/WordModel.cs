using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public class WordModel : LexicalModel<Word>
    {
        public WordModel(Word word) : base(word) { }

        public PhraseModel PhraseModel { get; set; }
    }
}