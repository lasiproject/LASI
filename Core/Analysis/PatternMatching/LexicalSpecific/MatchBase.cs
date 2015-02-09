using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching
{
    /// <summary>
    /// Represents a type for the representation and free form structuring of Type based Pattern Matching expressions 
    /// which match over a value of Type T and does not yield a result. 
    /// </summary>
    /// <typeparam name="T">
    /// The Type of the value which the the Pattern Matching expression will match with.
    /// </typeparam>
    /// <summary>
    /// Provides for the construction of flexible Typed Pattern Matching expressions.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class MatchBase<T> where T : class, ILexical
    {
        /// <summary>
        /// Initializes a new instance of the MatchBase&lt;T&gt; class which will match against the given value.
        /// </summary>
        /// <param name="value">The value to match against.</param>
        protected MatchBase(T value)
        {
            Value = value;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        protected bool Matched { get; set; }
        /// <summary>
        /// Gets or sets the value being matched against.
        /// </summary>
        protected T Value { get; }
        #endregion
    }
}
