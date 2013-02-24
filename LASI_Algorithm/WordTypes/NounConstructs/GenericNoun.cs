using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public abstract class GenericNoun : Noun
    {
        /// <summary>
        /// Initializes a new instances of the GenericNoun class.
        /// </summary>
        /// <param name="text">The literal text content of the GenericNoun</param>
        protected GenericNoun(string text)
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
                    EntityType = EntityType.Thing;
                    break;
            }
        }

    }
}
