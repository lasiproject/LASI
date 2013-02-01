
namespace LASI.Algorithm
{
    public interface IInderectObjectTaker
    {
        // void BindToIndirectObject(IActionObject verbObject);
        IEntity IndirectObject {
            get;
            set;
        }
    }
}
