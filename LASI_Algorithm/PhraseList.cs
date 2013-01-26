using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.DataStructures
{
    /// <summary>
    /// Represents a List of Phrase constructs
    /// </summary>
    class PhraseList : IEnumerable<Phrase>
    {
        private IEnumerable<Phrase> items;

        protected IEnumerable<Phrase> Items {
            get {
                return items;
            }
            set {
                items = value;
            }
        }
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


        public IEnumerator<Phrase> GetEnumerator() {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}