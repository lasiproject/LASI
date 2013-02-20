
namespace LASI.Algorithm
{
    public interface IPrepositionLinkable : ILexical
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
