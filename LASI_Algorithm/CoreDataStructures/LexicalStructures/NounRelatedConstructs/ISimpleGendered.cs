using System;
namespace LASI.Core
{
    public interface IGendered : IEntity
    {
        ComparativeHeuristics.Gender Gender { get; }
    }
}
