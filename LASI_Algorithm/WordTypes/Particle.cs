using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    public class Particle : Word
    {
        public Particle(string text)
            : base(text) {
        }

        public override string ToString() {
            return String.Format("{0} is a particle with text \"{1}\"",base.ToString(), Text);
        }

        //TODO: Implement any additional Particle specific methods or properties.
    }
}
