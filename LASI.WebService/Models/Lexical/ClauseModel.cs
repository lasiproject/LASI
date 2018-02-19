using LASI.WebService.Models.DocumentStructures;
using LASI.Core;
using LASI.Utilities;
using System;

namespace LASI.WebService.Models.Lexical
{
    public class ClauseModel : LexicalModel<Clause>
    {
        public ClauseModel(Clause clause) : base(clause) { }
        public SentenceModel Sentence { get; internal set; }
        public override ILexicalContextmenu Contextmenu { get; }
        public override string DetailText => ModelFor
            .ToString()
            .SplitRemoveEmpty('\n', '\r')[0];
            //.Format((' ', ' ', ' '), s => s + "\n");
    }
}