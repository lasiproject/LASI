using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content.Serialization
{
    using ConcurrentLexicalIdMapping = System.Collections.Concurrent.ConcurrentDictionary<LASI.Core.ILexical, int>;
    using ILexical = Core.ILexical;
    using Interlocked = System.Threading.Interlocked;
    class NodeNameMapper
    {
        public string this[ILexical element] { get { return GetNodeName(element); } }
        private string GetNodeName(ILexical element)
        {
            return element != null ?
                element.GetType().Name + " " + elementIds.GetOrAdd(element, e => Interlocked.Increment(ref idGenerator)) :
                string.Empty;
        }
        private int idGenerator;
        private ConcurrentLexicalIdMapping elementIds = new ConcurrentLexicalIdMapping();
    }
}
