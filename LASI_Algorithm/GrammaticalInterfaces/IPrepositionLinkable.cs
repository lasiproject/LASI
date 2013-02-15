
namespace LASI.Algorithm
{
    public interface IPrepositionLinkable : LASI.Algorithm.ILexical
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
