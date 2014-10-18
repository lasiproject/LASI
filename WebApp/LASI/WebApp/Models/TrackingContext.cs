using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Core.Heuristics;

namespace LASI.WebApp.Support.Serialization
{
    internal class TrackingContext<TLexical> where TLexical : ILexical
    {
        public TrackingContext() { }

        public TLexical this[long index] {
            get { TLexical result; return elementsById.TryGetValue(index, out result) ? result : default(TLexical); }
            set {
                elementsById.GetOrAdd(index, key => {
                    idsByElement.GetOrAdd(value, index);
                    return value;
                });
            }
        }
        public long this[TLexical index] {
            get { return idsByElement.GetOrAdd(index, element => element.GetHashCode()); }
            set {
                idsByElement.GetOrAdd(index, key => {
                    elementsById.GetOrAdd(value.GetHashCode(), index);
                    return value;
                });
            }
        }
        private ConcurrentDictionary<long, TLexical> elementsById = new ConcurrentDictionary<long, TLexical>(Interop.ResourceMonitoring.Concurrency.Max, 1024 * 1024);
        private ConcurrentDictionary<TLexical, long> idsByElement = new ConcurrentDictionary<TLexical, long>(Interop.ResourceMonitoring.Concurrency.Max, 1024 * 1024);
    }
}