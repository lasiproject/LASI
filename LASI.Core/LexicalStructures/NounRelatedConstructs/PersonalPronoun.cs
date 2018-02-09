using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Core.Heuristics;

namespace LASI.Core
{
    /// <summary>
    /// Represents a Personal Pronoun such as he, she, it, them, or both.
    /// This is a broad and highly contextual category.
    /// </summary>
    public class PersonalPronoun : Pronoun
    {
        /// <summary>
        /// Initializes a new instance of the PersonalPronoun class.
        /// </summary>
        /// <param name="text">The text content of the PersonalPronoun.</param>
        public PersonalPronoun(string text) : base(text) { }
        /// <summary>
        /// Gets the EntityKind of the PersonalPronoun
        /// </summary>
        public override EntityKind EntityKind =>
            Gender.IsFemale() ? EntityKind.PersonFemale : Gender.IsMale() ? EntityKind.PersonMale : base.EntityKind;
    }
}
