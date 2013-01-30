
namespace LASI.Algorithm
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
