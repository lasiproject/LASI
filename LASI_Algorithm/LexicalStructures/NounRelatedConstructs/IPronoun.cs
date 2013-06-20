using System;

namespace LASI.Algorithm
{
    public interface IPronoun : IEntity
    {
        IEntityGroup BoundEntity {
            get;
        }
        void BindToEntity(IEntity target);

        bool IsBound {
            get;
        }
    }
}
