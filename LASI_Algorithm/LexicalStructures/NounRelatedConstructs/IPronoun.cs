using System;

namespace LASI.Algorithm
{
    public interface IPronoun : IEntity
    {
        IEntity BoundEntity {
            get;
        }
        void BindToEntity(IEntity target);
        PronounKind PronounKind {
            get;
        }
        bool IsBound {
            get;
        }
    }
}
