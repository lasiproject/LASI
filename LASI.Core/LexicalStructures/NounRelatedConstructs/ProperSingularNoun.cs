using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Core
{
    /// <summary>
    /// Represents a Proper Singular Noun.
    /// </summary>
    public class ProperSingularNoun : ProperNoun, ISimpleGendered
    {
        /// <summary>
        /// Initializes a new instance of the ProperSingularNoun class.
        /// </summary>
        /// <param name="text">The text content of the ProperSingularNoun.</param>
        public ProperSingularNoun(string text)
            : base(text)
        {
            EntityKind = text.All(c => char.IsUpper(c) || c == '.')
                ? EntityKind.Organization
                : gender.GetValueOrDefault().IsMaleOrFemale()
                ? EntityKind.Person
                : EntityKind;
        }
        Gender? gender;
        /// <summary>
        /// Gets the Gender value indicating the likely gender of the ProperNoun.
        /// </summary>
        public virtual Gender Gender
        {
            get
            {
                if (gender is null)
                {
                    gender = DetermineGender();
                }

                return (Gender)gender;

                Gender DetermineGender()
                {
                    return this.IsFemaleFirstName()
                                    ? Gender.Female
                                    : this.IsMaleFirstName()
                                    ? Gender.Male
                                    : IsLastName
                                    || EntityKind == EntityKind.Organization
                                    || EntityKind == EntityKind.Location
                                    || EntityKind == EntityKind.Activity
                                    ? Gender.Neutral
                                    : Gender.Undetermined;
                }
            }
        }

    }
}
