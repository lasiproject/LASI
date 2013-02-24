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
                    EntityType = EntityType.Person;
                    break;
                case "location":
                    EntityType = EntityType.Location;
                    break;
                case "organization":
                    EntityType = EntityType.Organization;
                    break;
                default:
                    EntityType = EntityType.Person;
                    break;
            }
        }

    }

}
