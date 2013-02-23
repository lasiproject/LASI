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
        protected override void ProcessEntityTypeInfo(string found) {
            switch (found) {
                case "person":
                    EntityType = EntityKind.Person;
                    break;
                case "location":
                    EntityType = EntityKind.Location;
                    break;
                case "organization":
                    EntityType = EntityKind.Organization;
                    break;
                default:
                    EntityType = EntityKind.Person;
                    break;
            }
        }

    }

}
