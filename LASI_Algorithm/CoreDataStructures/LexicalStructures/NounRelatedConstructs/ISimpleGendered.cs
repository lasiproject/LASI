using System;
namespace LASI.Algorithm
{
    public interface IGendered : IEntity
    {
        LexicalLookup.Gender Gender { get; }
    }
}
