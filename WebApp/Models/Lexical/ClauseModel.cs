using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core;

namespace LASI.WebApp.Models.Lexical
{
    public class ClauseModel : LexicalModel
    {
        public ClauseModel(Clause clause) : base(clause) { }

        public SentenceModel SentenceModel { get; internal set; }
    }
}