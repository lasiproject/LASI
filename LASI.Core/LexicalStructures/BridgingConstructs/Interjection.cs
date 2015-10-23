using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace LASI.Core
{
    /// <summary>
    /// <para> Represents interjection words, such as "by-jove!" </para>
    /// <para> Not common in strategic documents, but they are necessary to properly map to the Tagset. </para>
    /// <para> And its nice to write "by-jove!" in documentation. </para>
    /// </summary>
    public class Interjection : Word
    {
        /// <summary>
        /// Initializes a new instance of the Interjection class.
        /// </summary>
        /// <param name="text">The  text content of the Interjection.</param>
        public Interjection(string text)
            : base(text) {
        }
    }
}
