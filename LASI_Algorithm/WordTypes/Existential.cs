using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class Existential : Word
    {
        public Existential(string text)
            : base(text) {
        }

        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
