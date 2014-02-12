
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// This class is currently experimental and is not a tier in the Document objects created by the tagged file parsers
    /// Represents a clause which provides discriptive quantitative or qualitative specification.
    /// </summary>
    public class SubordinateClause : Clause, IDescriptor, IAdverbial
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SubordinateClause class, by composing the given linear sequence of componentPhrases.
        /// </summary>
        /// <param name="composed">The linear sequence of Phrases which compose to form the Clause.</param>
        public SubordinateClause(IEnumerable<Phrase> composed)
            : base(composed) {

        }

        #endregion

        #region Methods



        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Verbial construct which the subordinate clause modifies.
        /// </summary>
        public IAdverbialModifiable Modifies {
            //get;
            //set;
            get {
                return modifies;
            }
            set {
                if (described == null)
                    modifies = value;
                else
                    throw new ConflictingClauseRoleException(
                        string.Format(@"Cannot bind {0}\n
                                        as a descriptive modifier of {1}\n
                                        because it is already indicated as a Verbal
                                        descriptive modifier of\n{2}", this, value, described));
            }
        }
        /// <summary>
        /// Gets or sets the IDescribable construct which the subordinate clause describes.
        /// </summary>
        public IEntity Describes {

            get {
                return described;
            }
            set {
                if (modifies == null)
                    described = value;
                else
                    throw new ConflictingClauseRoleException(
                        string.Format(@"Cannot bind {0}\n
                                        as an Entity descriptive modifier of {1}\n
                                        because it is already indicated as an Action
                                        descriptive modifier of\n{2}", this, value, modifies));
            }
        }
        #endregion

        #region Fields
        private IEntity described;
        private IAdverbialModifiable modifies;
        #endregion

    }
}
