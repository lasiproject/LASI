
namespace LASI.DataRepresentation
{
    public interface IObjectTaker
    {
        //void BindToDirectObject(IActionObject verbObject);
        IActionObject DirectObject {
            get;
            set;
        }
    }

}
