﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class ToLinker : Word, IPrepositional
    {
        public ToLinker(string text)
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
