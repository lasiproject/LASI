
namespace LASI.DataRepresentation
{

    public interface IActionSubject
    {
        IIntransitiveAction SubjectOf {
            get;
            set;
        }
    }
}
