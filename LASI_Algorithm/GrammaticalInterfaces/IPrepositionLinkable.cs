
namespace LASI.DataRepresentation
{
    public interface IPrepositionLinkable
    {
        IPrepositional LeftLinkedPrepositional {
            get;
            set;
        }
        IPrepositional RightLinkedPrepositional {
            get;
            set;
        }
    }
}
