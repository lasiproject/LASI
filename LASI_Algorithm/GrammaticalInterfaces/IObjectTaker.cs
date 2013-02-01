
namespace LASI.Algorithm
{
    public interface IObjectTaker
    {
        //void BindToDirectObject(IActionObject verbObject);
        IEntity DirectObject {
            get;
            set;
        }
    }

}
