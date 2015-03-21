using System;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>Determines how a match should be applied to a sentence or sentence fragment.</summary>
    public enum ContinuationMode
    {
        /// <summary>After a successful match, no continuation will be performed.</summary>
        None = 0,

        /// <summary>
        /// After a successful match, the same patterns will be applied to the remainder of the
        /// sentence or sentence fragment.
        /// </summary>
        Recursive,
    }
}