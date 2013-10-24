using System;
namespace LASI.Algorithm
{
    public interface IGendered : IEntity
    {
        ComparativeHeuristics.Gender Gender { get; }
    }
}
