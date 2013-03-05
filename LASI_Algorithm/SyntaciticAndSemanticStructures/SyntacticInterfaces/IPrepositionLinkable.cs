
namespace LASI.Algorithm
{
    public interface IPrepositionLinkable : ILexical
    {
        IPrepositional PrepositionOnLeft {
            get;
            set;
        }
        IPrepositional PrepositionOnRight {
            get;
            set;
        }
    }
}
