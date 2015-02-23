namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Descriptive constructs which descriptively modify Entity constructs.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IDescriptor interface provides 
    /// for generalization and abstraction over word and Phrase types.
    /// </summary>
    /// <seealso cref="IEntity"/>
    public interface IDescriptor : ILexical, IAdverbialModifiable, IAttributive<IEntity>
    {
        /// <summary>
        /// Gets or sets the Entity which the Descriptor instance describes.
        /// </summary>
        IEntity Describes {
            get;
            set;
        }
    }
}
