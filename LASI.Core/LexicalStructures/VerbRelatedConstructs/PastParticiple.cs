﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Represents a verb in its past-participle-tense.
    /// </summary>
    public class PastParticiple : Verb
    {
        /// <summary>
        /// Initializes a new instance of the PastParticipleVerb class.
        /// </summary>
        /// <param name="text">The text content of the PastParticipleVerb.</param>
        public PastParticiple(string text)
            : base(text)
        {
        }
    }
}
