using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Personal Pronoun such as he, she, it, or they. 
    /// </summary>
    public class PersonalPronoun : Pronoun
    {
        /// <summary>
        /// Initializes a new instance of the PersonalPronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the PersonalPronoun.</param>
        public PersonalPronoun(string text)
            : base(text) {
            var t = text.ToLower();
            PronounKind = t == "him" || t == "he" ? PronounKind.Male : t == "her"
                || t == "she" ? PronounKind.Female : t == "it" || t == "that" ||
                t == "this" ? PronounKind.GenderNeurtral : PronounKind.Ambiguous;
        }
    }
}
