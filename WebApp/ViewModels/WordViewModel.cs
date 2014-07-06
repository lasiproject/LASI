using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.ViewModels
{
    public class WordViewModel : LexicalElementViewModel
    {
        public WordViewModel(Word word) : base(word) { }
    }
}