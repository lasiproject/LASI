using System.Collections.Generic;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Defines the role of a an Entity which represents a group of entities, such as an organization which has several branches.
    /// A class which implemenets this interface must provide all of the behaviors of an entity and, additionally, provide all of the behaviors of an IEnumerable collection of Entities.
    /// </summary>
    interface IEntityGroup : IEntity, IEnumerable<IEntity>
    {

    }
}
