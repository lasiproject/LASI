
namespace LASI.DataRepresentation
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
