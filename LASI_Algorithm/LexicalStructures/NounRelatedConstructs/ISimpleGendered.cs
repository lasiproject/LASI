using System;
namespace LASI.Algorithm
{
    interface ISimpleGendered : IEntity
    {
        Lookup.Gender Gender { get; }
    }
}
