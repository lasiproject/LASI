using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Utilities;

namespace LASI.WebService.Models.Lexical
{
    public class PhraseModel : LexicalModel<Phrase>
    {
        public PhraseModel(Phrase phrase) : base(phrase)
        {
            Contextmenu = ContextmenuFactory.Create(ModelFor);
            var delimiters = (' ', ' ', ' ');
            DetailText = ModelFor
                .ToString()
                .SplitRemoveEmpty('\n', '\r').FirstOrDefault();
                //.Format(delimiters, s => s + "\n");
            Words = ModelFor.Words.Select(w => new WordModel(w));
        }
        public sealed override ILexicalContextmenu Contextmenu { get; }
        public sealed override string DetailText { get; }
        public IEnumerable<ILexicalModel<Word>> Words { get; }
    }
}
