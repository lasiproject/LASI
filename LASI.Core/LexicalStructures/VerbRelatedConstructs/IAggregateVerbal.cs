using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for an IVerbal which represents a group of IVerbals. </para>
    /// <para> A class which implements this interface must provide all of the behaviors of an IVerbal and, additionally, provide all of the behaviors
    /// of an IEnumerable collection of IVerbal. </para>
    /// </summary>
    public interface IAggregateVerbal : IVerbal, IAggregateLexical<IVerbal>
    {
    }
}
