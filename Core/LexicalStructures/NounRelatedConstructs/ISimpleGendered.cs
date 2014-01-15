using LASI.Core.Heuristics;

namespace LASI.Core
{
    /// <summary>
    /// <para>  Represents an Entity which by nature possesses a manifest, trivially computable gender attribute. </para>
    /// <para>  Along with the other interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies. </para>
    /// </summary>
    public interface ISimpleGendered : IEntity
    {
        /// <summary>
        /// Gets the Gender of the IGendered.
        /// </summary>
        Gender Gender { get; }
    }
}
