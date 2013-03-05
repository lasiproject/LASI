
namespace LASI.Algorithm
{
    public interface IObjectTaker
    {
        void BindDirectObject(IEntity directObject);
        System.Collections.Generic.IEnumerable<IEntity> DirectObjects {
            get;
        }

    }

}
