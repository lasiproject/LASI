using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Binding
{
    class InvalidStateTransitionException : Exception
    {
        public InvalidStateTransitionException(int stateNumber, ILexical errorOn)
            : base(String.Format("Invalid Transition\nAt State {0}\nOn {1}", stateNumber, errorOn)) {
        }
        public InvalidStateTransitionException(string stateName, ILexical errorOn)
            : base(String.Format("Invalid Transition\nAt State {0}\nOn {1}", stateName, errorOn)) {
        }
    }
}
