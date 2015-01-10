using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// This class is currently experimental and is not a tier in the Document objects created by
    /// the tagged file parsers Represents a clause which provides descriptive quantitative or
    /// qualitative specification.
    /// </summary>
    public class SubordinateClause : Clause, IDescriptor, IAdverbial
    {
        public IEnumerable<IAdverbial> AttributedBy { get { throw new NotImplementedException(); } }


        IVerbal IAttributive<IVerbal>.AttributedTo { get { throw new NotImplementedException(); } }

        IDescriptor IAttributive<IDescriptor>.AttributedTo { get { throw new NotImplementedException(); } }
        public IEntity AttributedTo { get { throw new NotImplementedException(); } }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SubordinateClause class, by composing the given linear
        /// sequence of componentPhrases.
        /// </summary>
        /// <param name="composed">
        /// The linear sequence of Phrases which compose to form the Clause.
        /// </param>
        public SubordinateClause(IEnumerable<Phrase> composed) : base(composed) { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Attaches an IAdverbial as a modifier of the SubordinateClause.
        /// </summary>
        /// <param name="modifier">
        /// The modifier to attach.
        /// </param>
        public void ModifyWith(IAdverbial modifier) {
            throw new NotImplementedException();
        }

        #endregion Methods

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
                if (describes == null) {
                    modifies = value;
                } else {
                    throw new ConflictingClauseRoleException(
                        string.Format(@"Cannot bind {0}\n
                                        as a descriptive modifier of {1}\n
                                        because it is already indicated as a Verbal
                                        descriptive modifier of\n{2}", this, value, describes));
                }
            }
        }

        /// <summary>
        /// Gets or sets the IDescribable construct which the subordinate clause describes.
        /// </summary>
        public IEntity Describes {
            get { return describes; }
            set {
                if (modifies == null) {
                    describes = value;
                } else {
                    throw new ConflictingClauseRoleException(
                        string.Format(@"Cannot bind {0}\n
                                        as an Entity descriptive modifier of {1}\n
                                        because it is already indicated as an Action
                                        descriptive modifier of\n{2}", this, value, modifies));
                }
            }
        }

        /// <summary>
        /// Gets the sequence of IAdverbial constructs which modify the SubordinateClause.
        /// </summary>
        public IEnumerable<IAdverbial> AdverbialModifiers {
            get {
                return adverbialModifiers;
            }
        }


        #endregion Properties

        #region Fields

        private IEntity describes;
        private IAdverbialModifiable modifies;
        private HashSet<IAdverbial> adverbialModifiers = new HashSet<IAdverbial>();

        #endregion Fields
    }
}