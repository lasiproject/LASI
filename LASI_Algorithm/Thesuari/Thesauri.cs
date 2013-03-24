using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LASI.Algorithm.Thesauri
{
    public static class ThesaurusManager
    {
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        static ThesaurusManager() {
            NounThesaurus = new NounThesaurus(nounThesaurusFilePath);
            NounThesaurus.Load();
            VerbThesaurus = new VerbThesaurus(verbThesaurusFilePath);
            VerbThesaurus.Load();
        }
        public static IEnumerable<string> Lookup(Verb verb) {
            return VerbThesaurus[verb];
        }
        public static IEnumerable<string> Lookup(Noun verb) {
            throw new NotImplementedException();
        }
        public static NounThesaurus NounThesaurus {
            get;
            private set;
        }

        public static VerbThesaurus VerbThesaurus {
            get;
            private set;
        }
    }
}
