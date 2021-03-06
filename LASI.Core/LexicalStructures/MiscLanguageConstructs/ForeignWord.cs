﻿using System;


namespace LASI.Core
{
    /// <summary>
    /// Represents a foriegn word embedded in an english written work.
    /// </summary>
    public class ForeignWord : Word
    {
        /// <summary>
        /// Initializes an instance of the ForeignWord class.
        /// </summary>
        /// <param name="text">The text content of the ForeignWord.</param>
        public ForeignWord(string text) : base(text) { }
        /// <summary>
        /// Gets or sets the equivalent English syntactic type if it can be inferred from the ForeignWord's usage.
        /// </summary>
        public virtual Type UsedAsType
        {
            get;
            set;
        }
    }
}