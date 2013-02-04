using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class Particle : Word, IPrepositional
    {
        public Particle(string text)
            : base(text) {
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
