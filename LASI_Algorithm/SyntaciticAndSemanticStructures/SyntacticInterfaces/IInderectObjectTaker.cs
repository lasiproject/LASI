
namespace LASI.Algorithm
{
    public interface IInderectObjectTaker
    {
        void BindIndirectObject(IEntity indirectObject);
        System.Collections.Generic.IEnumerable<IEntity> IndirectObjects {
            get;
        }
    }
}
