using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis
{
    static public class Weighter
    {
        /// <summary>
        /// Weighting algorithm assigns weight to each word and phrase in a document
        /// </summary>
        /// <param name="doc"></param>
        static public void weight(Document doc)
        {
            var wordsByCount = from w in doc.Words group w by new { w.Type, w.Text };
            var phraseByCount = from p in doc.Phrases group p by new { Type = p.GetType(), p.Text };
            var nounsynonymgroups = from w in doc.Words.GetNouns()
                                    let synstrings = Thesauri.ThesaurusManager.NounThesaurus[w]
                                    from t in doc.Words.GetNouns()
                                    where synstrings.Contains(t.Text)
                                    group t by w;
            var verbsynonymgroups = from w in doc.Words.GetVerbs()
                                    let synstrings = Thesauri.ThesaurusManager.VerbThesaurus[w]
                                    from t in doc.Words.GetVerbs()
                                    where synstrings.Contains(t.Text)
                                    group t by w;


            foreach (var grp in wordsByCount)
            {
                foreach (var w in grp)
                {
                    w.Weight = grp.Count();
                }
            }

            foreach (var grp in phraseByCount)
            {
                foreach (var p in grp)
                {
                    p.Weight = grp.Count();
                }
            }
            

        }
    }
}
