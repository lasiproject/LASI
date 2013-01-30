
namespace LASI.DataRepresentation
{
    public interface IPrepositional
    {
        void LinkToLeft(IPrepositionLinkable toLink);
        void LinkToRight(IPrepositionLinkable toLink);
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
