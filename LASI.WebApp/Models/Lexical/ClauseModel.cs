using LASI.WebApp.Models.DocumentStructures;
using LASI.Core;
using System;
using LASI.Utilities;

namespace LASI.WebApp.Models.Lexical
{
    public class ClauseModel : LexicalModel<Clause>
    {
        public ClauseModel(Clause clause) : base(clause) { }
        public SentenceModel Sentence { get; internal set; }
        public override ILexicalContextmenu Contextmenu { get; }
        public override string DetailText => ModelFor.ToString().SplitRemoveEmpty('\n', '\r').Format(Tuple.Create(' ', ' ', ' '), s => s + "\n");
    }
}