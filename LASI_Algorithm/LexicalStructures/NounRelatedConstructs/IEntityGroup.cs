using System.Collections.Generic;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role  requirements for an Entity which represents entity group of entities, such as an organization which has several branches.
    /// entity class which implemenets this interface must provide all of the behaviors of an entity and, additionally, provide all of the behaviors of an IEnumerable collection of Entities.
    /// </summary>
    public interface IEntityGroup : IEntity, IEnumerable<IEntity>
    {

    }
}
