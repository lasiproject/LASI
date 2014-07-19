using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core;
using LASI.WebApp;
using Newtonsoft.Json.Linq;

namespace LASI.WebApp.Models.Lexical
{
    public class PhraseModel : LexicalModel
    {
        public PhraseModel(Phrase phrase) : base(phrase) {

            ContextMenuJson = phrase.GetJsonMenuData();
            Phrase.VerboseOutput = true;
            DetailText = phrase.ToString().SplitRemoveEmpty('\n', '\r').Format(Tuple.Create(' ', ' ', ' '), s => s + "\n");
            WordViewModels = phrase.Words.Select(word => new WordModel(word));
        }
        public string ContextMenuJson { get; private set; }
        public string DetailText { get; private set; }
        public IEnumerable<WordModel> WordViewModels { get; private set; }
    }
}
