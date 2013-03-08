using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LASI.Algorithm.Thesauri
{
    public static class Thesauri
    {
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        static Thesauri() {
            NounThesaurus = new LASI.Algorithm.Thesauri.NounThesaurus(nounThesaurusFilePath);
            NounThesaurus.Load();
            VerbThesaurus = new LASI.Algorithm.Thesauri.VerbThesaurus(verbThesaurusFilePath);
            VerbThesaurus.Load();
        }

        public static LASI.Algorithm.Thesauri.NounThesaurus NounThesaurus {
            get;
            private set;
        }

        public static LASI.Algorithm.Thesauri.VerbThesaurus VerbThesaurus {
            get;
            private set;
        }
    }
}
