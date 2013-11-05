using System;
namespace LASI.Core
{
    /// <summary>
    /// Represents an Entity which by nature possesses a manifest gender. 
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies.
    /// </summary>
    public interface IGendered : IEntity
    {
        /// <summary>
        /// Gets the Gender of the IGendered.
        /// </summary>
        ComparativeHeuristics.Gender Gender { get; }
    }
}
