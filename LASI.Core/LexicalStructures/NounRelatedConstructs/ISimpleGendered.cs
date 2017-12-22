using LASI.Core.Heuristics;

namespace LASI.Core
{
    /// <summary>
    /// <para>  Represents an Entity which by nature possesses a manifest, trivially computable gender attribute. </para>
    /// <para>  Along with the other interfaces in the Syntactic Interfaces Library, the ISimpleGendered interface provides for generalization and abstraction over many otherwise disparate element types and Type hierarchies. </para>
    /// </summary>
    public interface ISimpleGendered : IEntity
    {
        /// <summary>
        /// The Gender of the ISimpleGendered.
        /// </summary>
        Gender Gender { get; }
    }
}
