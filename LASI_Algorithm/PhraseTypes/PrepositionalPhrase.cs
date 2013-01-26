using System;
using System.Collections.Generic;

namespace LASI.Algorithm
{
    public class PrepositionalPhrase : Phrase, IPrepositional
    {
        public PrepositionalPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }



        public virtual void LinkToLeft(IPrepositionLinkable toLink) {
            throw new NotImplementedException();
        }

        public virtual void LinkToRight(IPrepositionLinkable toLink) {
            throw new NotImplementedException();
        }

        public virtual IPrepositionLinkable RightLinked {
            get;
            set;
        }

        public virtual IPrepositionLinkable LeftLinked {
            get;
            set;
        }

        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
