using System.Collections.Generic;

namespace LASI.Algorithm
{
    public interface IReferenciable
    {
        void BindPronoun(IEntityReferencer pro);

        IEnumerable<IEntityReferencer> IndirectReferences {
            get;
        }
    }
}
