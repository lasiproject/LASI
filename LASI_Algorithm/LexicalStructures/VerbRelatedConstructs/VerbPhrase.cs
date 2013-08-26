﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Verb Phrase, a Phrase with the syntactic role of a verb.
    /// </summary>
    public class VerbPhrase : Phrase, IVerbal, IAdverbialModifiable, IModalityModifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VerbPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the VerbPhrase.</param>
        public VerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {

            Tense = composedWords.GetVerbs().Any() ?
                (from v in composedWords.GetVerbs()
                 group v.Tense by v.Tense into tenseGroup
                 orderby tenseGroup.Count()
                 select tenseGroup).First().Key : VerbMorph.Base;
        }

        #endregion
 
        #region Methods

        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the Verb.
        /// </summary>
        /// <param name="adv">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public void ModifyWith(IAdverbial adv) {
            modifiers.Add(adv);
            adv.Modifies = this;
        }
        /// <summary>
        /// Binds the VerbPhrase to an object via a propisitional construct such as a Prepositon or or PrepositionalPhrase.
        /// Example: He "ran" to work. where "work" is the object of ran via the prepositional construct "to"
        /// </summary>
        /// <param name="prepositional">The IPrepositional construct through which the Object is associated.</param>
        public virtual void AttachObjectViaPreposition(IPrepositional prepositional) {
            ObjectOfThePreoposition = prepositional.BoundObject;
            PrepositionalToObject = prepositional;
        }
        /// <summary>
        /// Binds the given Entity as a subject of the VerbPhrase instance.
        /// </summary>
        /// <param name="subject">The Entity to attach to the VerbPhrase as a subject.</param>
        public virtual void BindSubject(IEntity subject) {
            subjects.Add(subject);
            subject.SubjectOf = this;
        }

        /// <summary>
        /// Binds the given Entity as a direct object of the VerbPhrase instance.
        /// </summary>
        /// <param name="directObject">The Entity to attach to the VerbPhrase as a direct object.</param>
        public virtual void BindDirectObject(IEntity directObject) {
            directObjects.Add(directObject);
            directObject.DirectObjectOf = this;
            if (IsPossessive) {
                foreach (var subject in this.Subjects) {
                    subject.AddPossession(directObject);
                }
            } else if (IsClassifier) {
                foreach (var subject in this.Subjects) {
                    AliasDictionary.DefineAlias(subject, directObject);
                }
            }
        }
        /// <summary>
        /// Binds the given Entity as an indirect object of the VerbPhrase instance.
        /// </summary>
        /// <param name="indirectObject">The Entity to attach to the VerbPhrase as an indirect object.</param>
        public virtual void BindIndirectObject(IEntity indirectObject) {
            indirectObjects.Add(indirectObject);
            indirectObject.IndirectObjectOf = this;
        }

        /// <summary>
        /// Returns a string representation of the VerbPhrase.
        /// </summary>
        /// <returns>A string representation of the VerbPhrase.</returns>
        public override string ToString() {
            if (Phrase.VerboseOutput) {

                var result = base.ToString();
                foreach (var s in Subjects) {
                    result += s != null ? "\n\tSubject: " + s.ToString() : "";
                }
                foreach (var d in DirectObjects) {
                    result += d != null ? "\n\tDirect Object:" + d.ToString() : "";
                }
                foreach (var i in IndirectObjects) {
                    result += i != null ? "\n\tIndirect Object: " + i.ToString() : "";
                }
                if (ObjectOfThePreoposition != null) {
                    result += "\n\tVia Preposition Object" + ObjectOfThePreoposition.ToString();
                }
                foreach (var m in modifiers) {
                    result += modifiers.Count > 0 ? "\n\tModifier: " + m.ToString() : "";

                }
                return result;
            } else
                return base.ToString();
        }


        /// <summary>
        /// Determines if the VerbPhrase implies a possession relationship. E.g. in the senetence 
        /// "They certainly have a lot of ideas." the VerbPhrase "certainly have" asserts a possessor possessee relationship between "They" and "a lot of ideas".
        /// </summary>
        /// <returns>True if the VerbPhrase is a possessive relationship specifier, false otherwise.</returns>
        protected virtual bool DetermineIsPossessive() {
            isPossessive = Words.GetVerbs().Any() && Words.GetVerbs().Last().IsPossessive;
            return isPossessive ?? false;
        }
        /// <summary>
        /// Determines if the VerbPhrase acts as a classifier. E.g. in the senetence "Rodents are definitely prey animals." the VerbPhrase "are definitely" acts as a classification tool because it states that rodents are a subset of prey animals.
        /// </summary>
        /// <returns>True if the VerbPhrase is a classifier, false otherwise.</returns>
        protected virtual bool DetermineIsClassifier() {
            isClassifier = Words.GetVerbs().Any() && Words.GetVerbs().Last().IsClassifier;
            return isClassifier ?? false;
        }


        /// <summary>
        /// Return a value indicating if the Verb has any subjects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any Subjects bound to it, false otherwise.</returns>
        public bool HasSubject() {
            return subjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any subjects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any subjects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasSubject(Func<IEntity, bool> predicate) {
            return Subjects.Any(predicate) || Subjects.OfType<IPronoun>().Any(p => predicate(p.RefersTo));
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it, false otherwise.</returns>
        public bool HasDirectObject() {
            return DirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasDirectObject(Func<IEntity, bool> predicate) {
            return DirectObjects.Any(predicate) || DirectObjects.OfType<IPronoun>().Any(p => predicate(p.RefersTo));
        }
        /// <summary>
        /// Return a value indicating if the Verb has any indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct objects bound to it, false otherwise.</returns>
        public bool HasIndirectObject() {
            return IndirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any indirect objects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasIndirectObject(Func<IEntity, bool> predicate) {
            return IndirectObjects.Any(predicate) || IndirectObjects.OfType<IPronoun>().Any(p => predicate(p.RefersTo));
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct OR indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verb has any direct OR indirect objects bound to it, false otherwise.</returns>
        public bool HasObject() {
            return HasDirectObject() || HasIndirectObject();
        }
        /// <summary>
        /// Return a value indicating if the Verb has any direct OR indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verb has any direct OR indirect objects bound to it which match the given predicate function, false otherwise.</returns>
        public bool HasObject(Func<IEntity, bool> predicate) {
            return HasDirectObject(predicate) || HasIndirectObject(predicate);
        }



        #endregion

        #region Properties
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's subjects.
        /// </summary>
        public IAggregateEntity AggregateSubject { get { return new AggregateEntity(subjects); } }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's directobjects.
        /// </summary>
        public IAggregateEntity AggregateDirectObject { get { return new AggregateEntity(directObjects); } }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the VerbPhrase's indirectobjects.
        /// </summary>
        public IAggregateEntity AggregateIndirectObject { get { return new AggregateEntity(indirectObjects); } }
        /// <summary>
        /// Gets the collection of IAdverbial modifiers which modify the VerbPhrase.
        /// </summary>
        public IEnumerable<IAdverbial> Modifiers {
            get {
                return modifiers;
            }
        }
        /// <summary>
        /// Gets or sets the IDescriber which may, in certain contexts be bound to the IVerbal itself.
        /// </summary>
        public IDescriptor AdjectivalModifier { get; set; }
        /// <summary>
        /// Gets the prevailing Tense of the VerbPhrase.
        /// <see cref="VerbMorph"/>
        /// </summary>
        public VerbMorph Tense { get; protected set; }
        /// <summary>
        /// Gets or sets the ModalAuxilary word which modifies the VerbPhrase.
        /// </summary>
        public ModalAuxilary Modality { get; set; }
        /// <summary>
        /// Gets a value indicating wether or not the VerbPhrase has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        public bool IsPossessive {
            get {
                return isPossessive ?? DetermineIsPossessive();
            }
        }

        /// <summary>
        /// Gets a value indicating wether or not the VerbPhrase has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        public bool IsClassifier {
            get {
                return isClassifier ?? DetermineIsClassifier();
            }
        }

        /// <summary>
        /// Gets the subjects of the VerbPhrase.
        /// </summary>
        public IEnumerable<IEntity> Subjects { get { return subjects; } }
        /// <summary>
        /// Gets the direct objects of the VerbPhrase.
        /// </summary>
        public virtual IEnumerable<IEntity> DirectObjects { get { return directObjects; } }
        /// <summary>
        /// Gets the indirect objects of the VerbPhrase.
        /// </summary>
        public virtual IEnumerable<IEntity> IndirectObjects { get { return indirectObjects; } }
        //virtual IEntityGroup Subject { get { return new EntityGroup(subjects); } }
        //virtual IEntityGroup DirectObject { get { return new EntityGroup(directObjects); } }
        //virtual IEntityGroup IndirectObject { get { return new EntityGroup(indirectObjects); } }
        /// <summary>
        /// Gets the VerbPhrases's object, If the VerbPhrase has an object bound via a Prepositional. This can be any ILexical construct including a word, phrase, or clause.
        /// </summary>
        public ILexical ObjectOfThePreoposition { get; protected set; }
        /// <summary>
        /// Gets the IPrepositional object which links the VerbPhrase to the ObjectOfThePreoposition.
        /// </summary>
        public IPrepositional PrepositionalToObject { get; protected set; }


        #endregion

        #region Fields

        private HashSet<IAdverbial> modifiers = new HashSet<IAdverbial>();
        private HashSet<IEntity> subjects = new HashSet<IEntity>();
        private HashSet<IEntity> directObjects = new HashSet<IEntity>();
        private HashSet<IEntity> indirectObjects = new HashSet<IEntity>();
        private bool? isClassifier;
        private bool? isPossessive;
        #endregion


    }
}

