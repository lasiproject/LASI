
namespace LASI.Algorithm
{
    public interface IPrepositional : ILexical
    {
        IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        IPrepositionLinkable OnLeftSide {
            get;
            set;
        }
    }
}
