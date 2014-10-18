using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core;

namespace LASI.ContentSystem.Serialization
{
    class NodeNameMapper
    {
        public string this[ILexical element] { get { return GetNodeName(element); } }
        private string GetNodeName(ILexical element) {
            return element != null ?
                element.GetType().Name + " " + elementIds.GetOrAdd(element, e => System.Threading.Interlocked.Increment(ref idGenerator)) :
                string.Empty;
        }
        private int idGenerator;
        private System.Collections.Concurrent.ConcurrentDictionary<ILexical, int> elementIds = new System.Collections.Concurrent.ConcurrentDictionary<ILexical, int>();
    }
}
