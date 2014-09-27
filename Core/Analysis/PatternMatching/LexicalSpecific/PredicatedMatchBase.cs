using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.PatternMatching
{
    /// <summary>
    /// Provides for the representation and free-form structuring of a non result yielding Match expression which is predicated by an arbitrary condition.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    [DebuggerStepThrough]
    public abstract class PredicatedMatchBase<T> where T : class, ILexical
    {
        /// <summary>
        /// Initializes a new instance of the PredicatedMatchBase&lt;T;&gt; class.
        /// </summary>
        /// <param name="predicateSucceeded">A value indicating whether or not the proceding When clause succeeded.</param>
        protected PredicatedMatchBase(bool predicateSucceeded) { ConditionMet = predicateSucceeded; }
        #region Fields
        /// <summary>
        /// Gets a value indicating if condition upon which the match is predicated has was satisfied.
        /// </summary>
        /// <returns>True if condition upon which the match is predicated has was satisfied; false otherwise.</returns>
        protected bool ConditionMet { get; private set; }
        #endregion
    }
}
