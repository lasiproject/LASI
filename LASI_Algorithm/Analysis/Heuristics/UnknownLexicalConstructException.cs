using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Analysis.Heuristics
{
    class UnknownLexicalConstructException : Exception
    {
        private string p;

        public UnknownLexicalConstructException(string p) {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
