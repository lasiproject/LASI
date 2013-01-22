
namespace LASI.Algorithm
{

    public interface IActionSubject
    {
        IIntransitiveAction SubjectOf {
            get;
            set;
        }
    }
}
