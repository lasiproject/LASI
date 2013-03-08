using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public static class Thesauri
    {
        private const string nounThesaurusFilePath = @"..\..\..\..\WordNetThesaurusData\data.noun";
        private const string verbThesaurusFilePath = @"..\..\..\..\WordNetThesaurusData\data.verb";
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
