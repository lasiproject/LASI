using System.Collections.Generic;

namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role  requirements for an Entity which represents a group of entities, such as an organization which has several branches.
    /// A class which implemenets this interface must provide all of the behaviors of an Entity and, additionally, provide all of the behaviors of an IEnumerable collection of Entities.
    /// </summary>
    public interface IEntityGroup : IEntity, IEnumerable<IEntity>
    {

    }
}
