using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a word which is used to make existential assertions.
    /// For example, "there" is an existential word in the statements, "There exists a solution." and "There are five of them."
    /// </summary>
    public class Existential : Word
    {
        /// <summary>
        /// Initializes a new instance of the Existential class.
        /// </summary>
        /// <param name="text">The key text content of the Existential word.</param>
        public Existential(string text)
            : base(text) {
        }

    }
}
