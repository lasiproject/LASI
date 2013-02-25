using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    public abstract class PhraseTagsetMap
    {
        public abstract IReadOnlyDictionary<string, Func<IEnumerable<Word>, Phrase>> TypeDictionary {
            get;
        }
        public abstract Func<IEnumerable<Word>, Phrase> this[string tag] {
            get;
        }

        public abstract string this[Func<IEnumerable<Word>, Phrase> mappedConstructor] {
            get;
        }
    }
}
