using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for an Entity which represents a group of IEntities, such as an organization which has several branches. </para>
    /// <para> A class which implements this interface must provide all of the behaviors of an Entity and, additionally, provide all of the behaviors</para>
    /// <para> of an IEnumerable collection of Entities. </para>
    /// </summary>
    public interface IAggregateEntity : IEntity, IAggregateLexical<IEntity> { }
}
