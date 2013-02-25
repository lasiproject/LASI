using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    class EntityReferenceBinder
    {
        public EntityReferenceBinder(EntityStream stream) {
            Stream = stream;
        }

        private static void NextTyped(GenericSingularNoun entity) {
        }

        private static double ComputeLikelyhood() {
            throw new NotImplementedException();
        }

        public EntityStream Stream {
            get;
            protected set;
        }


        

        enum PronounGender
        {
            Male,
            Female,
            Thing,
            Group
        }
    }
}
