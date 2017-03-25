namespace LASI.Core
{
    /// <summary>
    /// <para>
    /// Defines the role requirements for constructs, generally Actions, which can be modified by
    /// modal words such as "can" or "shalt".
    /// </para>
    /// <para>
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IModalityModifiable
    /// interface provides for cross-axial generalization over lexical types.
    /// </para>
    /// </summary>
    public interface IModalityModifiable
    {
        /// <summary>Gets or sets the ModalAuxilary word which modifies this instance.</summary>
        ModalAuxilary Modality
        {
            get;
            set;
        }
    }
}