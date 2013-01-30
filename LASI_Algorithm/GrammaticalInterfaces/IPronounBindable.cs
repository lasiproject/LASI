using System.Collections.Generic;

namespace LASI.Algorithm
{
    public interface IReferenciable
    {
        void BindPronoun(IEntityReferencer pro);

        ICollection<IEntityReferencer> IndirectReferences {
            get;
        }
    }
}
