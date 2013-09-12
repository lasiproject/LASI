using System;
namespace LASI.Algorithm
{
    interface IGendered : IEntity
    {
        LexicalLookup.Gender Gender { get; }
    }
}
