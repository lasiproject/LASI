using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.ClauseTypes
{
    /// <summary>
    /// This class is currently experimental and is not a tier in the ParentDocument objects created by the tagged file parsers
    /// Represents a clause which provides discriptive quantitative or qualitative specification.
    /// </summary>
    public class SubordinateClause : Clause, IDescriber, IAdverbial
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SubordinateClause class, by composing the given linear sequence of phrases.
        /// </summary>
        /// <param name="phrases">The linear sequence of Phrases which compose to form the Clause.</param>
        public SubordinateClause(IEnumerable<Phrase> composed)
            : base(composed) {

        }
        /// <summary>
        /// Initializes a new instance of the SubordinateClause class, by composing the given linear sequence of Words.
        /// </summary>
        /// <param name="phrases">The linear sequence of Words which compose to form the Clause.</param>
        public SubordinateClause(IEnumerable<Word> words)
            : base(words) {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representation of the subordinate clause.
        /// </summary>
        /// <returns>a string representation of the subordinate clause.</returns>
        public override string ToString() {
            return base.ToString() +
                Described != null ?
                "\ndescribes" + Described.ToString() :
                Modiffied != null ?
                "\nmodifies: " + Modiffied.ToString() :
                String.Empty;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Verbial construct which the subordinate clause modifies.
        /// </summary>
        public IVerbial Modiffied {
            get;
            set;
            //            get {
            //                return _modiffied;
            //            }
            //            set {
            //                if (Described == null)
            //                    _modiffied = value;
            //                else
            //                    throw new ConflictingClauseRoleException(String.Format(@"Cannot bind {0}\n
            //                                        as an Action discriptive modifier of {1}\n
            //                                        because it is already indicated as an Action
            //                                        descriptive modifier of\n{2}", this, value, Described));
            //            }
        }
        /// <summary>
        /// Gets or sets the Entity construct which the subordinate clause describes.
        /// </summary>
        public IEntity Described {
            get;
            set;
            //            get {
            //                return _described;
            //            }
            //            set {
            //                if (Modiffied == null)
            //                    _described = value;
            //                else
            //                    throw new ConflictingClauseRoleException(
            //                        String.Format(@"Cannot bind {0}\n
            //                                        as an Entitiy discriptive modifier of {1}\n
            //                                        because it is already indicated as an Action
            //                                        descriptive modifier of\n{2}", this, value, Modiffied));
            //            }
        }
        #endregion

        #region Fields
        //private IEntity _described;
        //private IVerbial _modiffied;
        #endregion

    }
}
