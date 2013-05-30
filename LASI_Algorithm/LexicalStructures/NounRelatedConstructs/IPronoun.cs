using System;
namespace LASI.Algorithm.SyntacticInterfaces
{
    public interface IPronoun : IEntity
    {
        IEntity BoundEntity {
            get;
        }
        void BindToIEntity(IEntity target);
        PronounKind PronounKind {
            get;
        }
    }
}
