
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
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
        /// <summary>
        /// Initializes a new instance of the SubordinateClause class, by composing the given linear sequence of Words.
        /// </summary>
        /// <param name="words">The linear sequence of Words which compose to form the Clause.</param>
        public SubordinateClause(IEnumerable<Word> words)
            : base(words) {

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
                return _modified;
            }
            set {
                if (_described == null)
                    _modified = value;
                else
                    throw new ConflictingClauseRoleException(String.Format(@"Cannot bind {0}\n
                                                    as an Verbal discriptive modifier of {1}\n
                                                    because it is already indicated as an Action
                                                    descriptive modifier of\n{2}", this, value, _described));
            }
        }
        /// <summary>
        /// Gets or sets the IDescribable construct which the subordinate clause describes.
        /// </summary>
        public IDescribable Describes {

            get {
                return _described;
            }
            set {
                if (_modified == null)
                    _described = value;
                else
                    throw new ConflictingClauseRoleException(
                        String.Format(@"Cannot bind {0}\n
                                                    as an Entitiy discriptive modifier of {1}\n
                                                    because it is already indicated as an Action
                                                    descriptive modifier of\n{2}", this, value, _modified));
            }
        }
        #endregion

        #region Fields
        private IDescribable _described;
        private IAdverbialModifiable _modified;
        #endregion

    }
}
