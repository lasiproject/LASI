
namespace LASI.DataRepresentation
{
    public interface IInderectObjectTaker
    {
        // void BindToIndirectObject(IActionObject verbObject);
        IActionObject IndirectObject {
            get;
            set;
        }
    }
}
