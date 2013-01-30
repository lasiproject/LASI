
namespace LASI.Algorithm
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
