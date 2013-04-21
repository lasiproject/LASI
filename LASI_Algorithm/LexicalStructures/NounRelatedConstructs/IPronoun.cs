using System;
namespace LASI.Algorithm.LexicalStructures.NounRelatedConstructs
{
    public interface IPronoun : IEntity
    {
        LASI.Algorithm.IEntity BoundEntity {
            get;
        }
        void BindToIEntity(IEntity target);
        PronounKind PronounKind {
            get;
        }
    }
}
