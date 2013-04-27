using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm;
using LASI.Utilities;
using LASI.Algorithm.LexicalStructures;
using LASI.Algorithm.DocumentConstructs;
namespace LASI.Algorithm.Analysis.Binding
{
    public class PronounBinder
    {
        public void Bind(Document documennt) {

            foreach (var g in documennt.Paragraphs) {
                NounPhrase lastTarg = null;
                foreach (var f in g.Phrases.GetPhrasesAfter(lastTarg ?? g.Phrases.First()).GetNounPhrases().Where(n => !(n is PronounPhrase))) {
                    var pairs = from i in g.Phrases.GetPhrasesAfter(f).GetPronounPhrases()

                                select new {
                                    np = f,
                                    pro = i
                                };
                    foreach (var n in pairs.Where(r => r.pro.PronounKind == PronounKind.GenderNeurtral || r.pro.PronounKind == PronounKind.Inanimate))

                    //.GetNounPhrases().Where(
                    //   )

                {

                        if (n.pro != null) {
                            n.np.BindPronoun(n.pro);
                            n.pro.BindToIEntity(n.np);
                        }

                    }
                    lastTarg = f;
                }
            }
        }
        static bool RoleEquivalent(NounPhrase a, NounPhrase b) {

            return false;
        }




    }

}
