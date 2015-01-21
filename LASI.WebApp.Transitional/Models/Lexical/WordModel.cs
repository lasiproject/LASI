using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LASI.WebApp.Models.Lexical
{
    public class WordModel : LexicalModel<Core.Word>
    {
        public WordModel(Core.Word word) : base(word) { }

        public PhraseModel PhraseModel { get; set; }
    }
}