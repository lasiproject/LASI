using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Analysis.Binding
{
    public class UnknownEntityCompatibleWordTypeException : Exception
    {
        private string p;

        public UnknownEntityCompatibleWordTypeException(string p) {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
