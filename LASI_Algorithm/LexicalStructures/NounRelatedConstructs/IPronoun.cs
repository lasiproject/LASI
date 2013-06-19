using System;

namespace LASI.Algorithm
{
    public interface IPronoun : IEntity
    {
        IEntity BoundEntity {
            get;
        }
        void BindToTarget(IEntity target);
        PronounKind PronounKind {
            get;
        }
        bool IsBound {
            get;
        }
    }
}
