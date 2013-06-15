using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesuari.Experimental
{
    interface ISynonymLookup<T, R> : ILookup<T, R>
        where T : Word
        where R : T
    {
    }

    class VerbLookup : ISynonymLookup<Verb, Verb>
    {

        public bool Contains(Verb key) {
            throw new NotImplementedException();
        }

        public int Count {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Verb> this[Verb key] {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<IGrouping<Verb, Verb>> GetEnumerator() {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
