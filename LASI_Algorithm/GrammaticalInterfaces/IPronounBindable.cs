using System.Collections.Generic;

namespace LASI.DataRepresentation
{
    public interface IReferenciable
    {
        void BindPronoun(IEntityReferencer pro);

        ICollection<IEntityReferencer> IndirectReferences {
            get;
        }
    }
}
