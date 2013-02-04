using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class ParticlePhrase : Phrase, IPrepositional
    {
        public ParticlePhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }

        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        public void LinkToLeft(IPrepositionLinkable toLink) {
            throw new NotImplementedException();
        }

        public void LinkToRight(IPrepositionLinkable toLink) {
            throw new NotImplementedException();
        }

        public IPrepositionLinkable RightLinked {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public IPrepositionLinkable LeftLinked {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
