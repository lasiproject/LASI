using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    public class SynonymSet : IReadOnlyCollection<string>
    {
        public string IndexCode {
            get;
            set;
        }
        public List<string> SynIDCodes {
            get;
            set;
        }
        public List<string> AntIDCodes {
            get;
            set;
        }
        public override string ToString() {
            return SynIDCodes.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }


        public int Count {
            get {
                return SynIDCodes.Count;
            }
        }

        public IEnumerator<string> GetEnumerator() {
            return SynIDCodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
