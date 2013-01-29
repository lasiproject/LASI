using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    class TransitiveVerbList : VerbList, IEnumerable<TransitiveVerb>
    {
        public TransitiveVerbList(IEnumerable<TransitiveVerb> elements)
            : base(elements) {
            items = elements;
        }

        public TransitiveVerbList WithObject(Func<IActionObject, bool> predicate) {
            return (TransitiveVerbList)from V in TransitiveVerbs
                                       where V.DirectObject != null && predicate(V.DirectObject)
                                       select V;
        }

        public new IEnumerator<TransitiveVerb> GetEnumerator() {
            return TransitiveVerbs.OfType<TransitiveVerb>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
