using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Defines the categories of determiners.
    /// </summary>
    public enum DeterminerKind : byte
    {
        /// <summary>
        /// Definite determiners such as "the".
        /// </summary>
        Definite,
        /// <summary>
        /// Indefinite determiners such as "a" and "an".
        /// </summary>
        Indefinite
    }
}
