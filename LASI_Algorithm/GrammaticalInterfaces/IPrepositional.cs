
namespace LASI.Algorithm
{
    public interface IPrepositional : ILexical
    {
        IPrepositionLinkable RightLinked {
            get;
            set;
        }
        IPrepositionLinkable LeftLinked {
            get;
            set;
        }
    }
}
