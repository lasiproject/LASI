﻿namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a Lexical construct which has the effect of subordinating the elements which follow it. This behavior has many concrete forms. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the ISubordinator interface provides for generalization and abstraction over many otherwise disparate element types and Type hierarchies. </para>
    /// </summary>
    /// <seealso cref="SubordinateClauseBeginPhrase"/>
    public interface ISubordinator : ILexical
    {
        /// <summary>
        /// Gets or sets the Lexical construct which is subordinated by the Subordinator.
        /// </summary>
        ILexical Subordinates
        {
            get;
            set;
        }
    }
}
