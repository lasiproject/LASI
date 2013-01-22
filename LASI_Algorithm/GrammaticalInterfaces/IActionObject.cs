
namespace LASI.Algorithm
{
    public interface IActionObject
    {
        ITransitiveAction DirectObjectOf {
            get;
            set;
        }
        ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
    }
}
