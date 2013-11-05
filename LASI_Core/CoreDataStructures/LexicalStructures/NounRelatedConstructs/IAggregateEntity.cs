using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for an Entity which represents a group of IEntities, such as an organization which has several branches.
    /// A class which implemenets this interface must provide all of the behaviors of an Entity and, additionally, provide all of the behaviors of an IEnumerable collection of Entities.
    /// </summary>
    public interface IAggregateEntity : IEntity, IEnumerable<IEntity>, IAggregateLexical<IEntity> { }
}
