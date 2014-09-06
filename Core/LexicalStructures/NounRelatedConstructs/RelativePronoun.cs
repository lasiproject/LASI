using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents a Relative Pronoun such as "that", "which, "what" or "who".
    /// </summary>
    public class RelativePronoun : Word, IReferencer, ISubordinator
    {
        /// <summary>
        /// Initializes a new instance of the RelativePronoun class.
        /// </summary>
        /// <param name="text">The text content of the RelativePronoun.</param>
        public RelativePronoun(string text)
            : base(text) {
            RelativePronounKind = DetermineKind(this);
        }

        #region Methods
        /// <summary>
        /// Binds the RelativePronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target) {
            if (RefersTo == null || RefersTo.None()) {
                RefersTo = new AggregateEntity(new[] { target });
            } else {
                RefersTo = new AggregateEntity(RefersTo.Append(target));
            }
            EntityKind = RefersTo.EntityKind;
        }


        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of IEntity instances the RelativePronoun "Owns",
        /// and sets its owner to be the RelativePronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession) {
            if (IsBound) {
                RefersTo.AddPossession(possession);
            } else {
                possessed.Add(possession);
                possession.Possesser = this;
            }
        }
        /// <summary>
        /// Binds an EntityReferencer, generally a Pronoun or PronounPhrase to refer to the RelativePronoun.
        /// </summary>
        /// <param name="pro">The EntityReferency to Bind.</param>
        public void BindReferencer(IReferencer pro) {
            boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the RelativePronoun.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the RelativePronoun's descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Returns a string representation of the RelativePronoun.
        /// </summary>
        /// <returns>A string representation of the RelativePronoun.</returns>
        public override string ToString() {
            var result = base.Text;
            result += VerboseOutput ? " " + RelativePronounKind.ToString() : string.Empty;
            return result;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the IEntity which can be said to "own" the RelativePronoun.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Indicates whether or not the IPronoun is bound to an Entity.
        /// </summary>
        public bool IsBound {
            get { return RefersTo != null && RefersTo.Any(); }
        }
        /// <summary>
        /// Gets the RelativePronounKind of the RelativePronoun.
        /// </summary>
        public RelativePronounKind RelativePronounKind {
            get; private set;
        }
        /// <summary>
        /// Gets the Entity which the RelativePronoun references.
        /// </summary>
        public IAggregateEntity RefersTo { get; private set; }

        /// <summary>
        /// Gets the EntityKind; Person, Place, Thing, Organization, or Activity;  of the Noun.
        /// </summary>
        public EntityKind EntityKind { get; private set; }
        /// <summary>
        ///Gets or sets the IVerbal instance the RelativePronoun is the subject object of.
        /// </summary>
        public IVerbal SubjectOf { get; set; }
        /// <summary>
        ///Gets or sets the IVerbal instance the RelativePronoun is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; set; }
        /// <summary>
        ///Gets or sets the IVerbal instance the RelativePronoun is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; set; }



        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the RelativePronoun Instance.
        /// </summary>
        public IEnumerable<IReferencer> Referencers {
            get {
                return boundPronouns;
            }
        }

        /// <summary>
        /// Gets the collection of IDescriptors, generally Adjectives or AdjectivePhrases which describe the RelativePronoun.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors {
            get { return descriptors; }
        }
        /// <summary>
        /// Gets the collection of IEntity instances which the RelativePronoun can be said to "own".
        /// </summary>
        public IEnumerable<IPossessable> Possessions {
            get { return possessed; }
        }


        /// <summary>
        /// Gets or sets the Lexical construct which is subordinated by the RelativePronoun.
        /// </summary>
        public ILexical Subordinates { get; set; }


        #endregion


        private HashSet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        private HashSet<IPossessable> possessed = new HashSet<IPossessable>();
        private HashSet<IReferencer> boundPronouns = new HashSet<IReferencer>();


        private static RelativePronounKind DetermineKind(RelativePronoun relativePronoun) {
            var text = relativePronoun.Text.ToLower();
            return
                subjectRolePersonal.Contains(text) ? RelativePronounKind.SubjectRolePersonal :
                objectRoleEntity.Contains(text) ? RelativePronounKind.ObjectRoleEntity :
                objectRoleLocationals.Contains(text) ? RelativePronounKind.ObjectRoleLocational :
                objectRoleTemporals.Contains(text) ? RelativePronounKind.ObjectRoleTemporal :
                objectRoleExpositories.Contains(text) ? RelativePronounKind.ObjectRoleExpository :
                RelativePronounKind.Undetermined;
        }

        private static readonly string[] subjectRolePersonal = { "who", "that" };
        private static readonly string[] objectRoleEntity = { "whom", "which", "who", "that" };
        private static readonly string[] objectRoleLocationals = { "where" };
        private static readonly string[] objectRoleTemporals = { "when" };
        private static readonly string[] objectRoleExpositories = { "what", "why" };



    }
}
