
namespace LASI.Algorithm
{
    public interface IPrepositional : ILexical
    {
        /// <summary>
        /// Gets or sets the IPropositionLinkable construct on the Right side of the IPrepositional.
        /// </summary>
        IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IPropositionLinkable construct on the Left side of the IPrepositional.
        /// </summary>
        IPrepositionLinkable OnLeftSide {
            get;
            set;
        }
    }
}
