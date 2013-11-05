using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Binding
{
    class InvalidInfinitePhraseConstructedException : Exception
    {
        private string p;

        public InvalidInfinitePhraseConstructedException(string p) {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
