using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm;
using LASI.Utilities;
namespace LASI.Algorithm.Analysis.Binding
{
    public class PronounBinder
    {
        private Document SourceDocument;
        public void Bind(Document documennt) {
            SourceDocument = documennt;
            foreach (var word in documennt.Words) {
                new Switch(word)
                .Case<Pronoun>(p => {
                    Output.WriteLine(p.Text);
                }).Case<Noun>(n => {
                    Output.WriteLine(n.Text);
                }).Default<Word>(w => {
                    throw new InvalidOperationException();
                });
            }
        }
    }
}
