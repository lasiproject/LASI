using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class SynonymSet : IReadOnlyCollection<string>
    {
        public SynonymSet(IEnumerable<string> referencedSetIds, IEnumerable<string> memberWords, string index) {
            _members = memberWords.Distinct();
            _referencedIndexes = referencedSetIds;
            IndexCode = index;
        }
        public IEnumerable<string> Members {
            get {
                return _members;
            }
        }


        IEnumerable<string> _members;
        protected IEnumerable<string> _referencedIndexes;

        public IEnumerable<string> ReferencedIndexes {
            get {
                return _referencedIndexes;
            }
            set {
                _referencedIndexes = value;
            }
        }

        public string IndexCode {
            get;
            protected set;
        }

        public override string ToString() {
            return "[" + IndexCode + "] " + Members.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }


        public int Count {
            get {
                return Members.Count();
            }
        }

        public IEnumerator<string> GetEnumerator() {
            return Members.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
