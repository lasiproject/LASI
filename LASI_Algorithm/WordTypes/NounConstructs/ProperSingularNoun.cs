using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    public class ProperSingularNoun : ProperNoun
    {
        public ProperSingularNoun(string text)
            : base(text) {
        }
        protected override void ProcessKind(string found) {
            switch (found) {
                case "person":
                    KindOfEntity = EntityKind.Person;
                    break;
                case "location":
                    KindOfEntity = EntityKind.Location;
                    break;
                case "organization":
                    KindOfEntity = EntityKind.Organization;
                    break;
                default:
                    KindOfEntity = EntityKind.Person;
                    break;
            }
        }

    }

}
