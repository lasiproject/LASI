using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents a List of Phrase constructs
    /// </summary>
    class PhraseList : List<Phrase>
    {

        #region Constructors

        public PhraseList(IEnumerable<Phrase> phrases) {
            items = phrases;
        }
        public PhraseList() {
            items = new List<Phrase>(32);
        }
        #endregion

        public virtual WordList PhraseHeads {
            get {
                return (WordList) from P in items
                                  select P.HeadWord;
            }
        }

        private IEnumerable<Phrase> items;

        protected virtual IEnumerable<Phrase> Items {
            get {
                return items;
            }
            set {
                items = value;
            }
        }
    }
}