using System;
namespace LASI.Algorithm
{
    interface IGendered : IEntity
    {
        Lookup.Gender Gender { get; }
    }
}
