using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LASI.Algorithm;


namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class, shared properties, and shared behaviors for all noun words.
    /// </summary>
    public abstract class Noun : Word, IEntity, IDeterminable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Noun class.
        /// </summary>
        /// <param name="text">The key text content of the Noun.</param>
        protected Noun(string text)
            : base(text)
        {
            EntityKind = EntityKind.Unknown;
            EstablishKind();
        }



        #endregion

        #region Methods

        private void EstablishKind()
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<([^>]+)>");
            var found = regex.Match(Text).Value ?? "";
            Text = found.Length > 0 ? new string(Text.Skip(found.Length).TakeWhile(c => c != '<').ToArray()) : Text;
        }


        /// <summary>
        /// Binds the given Determiner to The Noun. 
        /// </summary>
        /// <param name="determiner">The Determiner which to bind.</param>
        public virtual void BindDeterminer(Determiner determiner)
        {
            determiner.Determines = this;
            Determiner = determiner;
        }

        /// <summary>
        /// Binds an EntityReferencer, generall a Pronoun or PronounPhrase to refer to the Noun.
        /// </summary>
        /// <param name="pro">The EntityReferency to Bind.</param>
        public virtual void BindPronoun(IPronoun pro)
        {
            _boundPronouns.Add(pro);
            pro.BindToTarget(this);
        }

        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the Noun.
        /// </summary>
        /// <param name="adjective">The IDescriptor instance which will be added to the Noun'subject descriptors.</param>
        public virtual void BindDescriptor(IDescriptor adjective)
        {
            adjective.Describes = this;
            _describedBy.Add(adjective);
        }

        public void AddPossession(IEntity possession)
        {
            if (!_possessed.Contains(possession))
                _possessed.Add(possession);
            possession.Possesser = this;
        }

        public bool Equals(IEntity other)
        {
            return this == other as Noun;
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the Noun Instance.
        /// </summary>
        public virtual IEnumerable<IPronoun> BoundPronouns
        {
            get
            {
                return _boundPronouns;
            }
        }

        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the Noun Instance.
        /// </summary>
        public virtual IEnumerable<IDescriptor> Descriptors
        {
            get
            {
                return _describedBy;
            }
        }


        /// <summary>
        ///Gets or sets the ITRansitiveAction instance, usually a Verb or VerbPhrase, which the Noun is the direct object of.
        /// </summary>
        public virtual IVerbal DirectObjectOf
        {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the Noun is the indirect object of.
        /// </summary>
        public virtual IVerbal IndirectObjectOf
        {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbal instance the Noun is the subject of.
        /// </summary>
        public virtual IVerbal SubjectOf
        {
            get;
            set;
        }

        /// <summary>
        /// Gets all of the IPossessable constructs which the Entity "owns".
        /// </summary>
        public virtual IEnumerable<IEntity> Possessed
        {
            get
            {
                return _possessed;
            }
        }

        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the Noun.
        /// </summary>
        public IEntity Possesser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Entity NounPointerSymbol; Person, Place, Thing, Organization, or Activity;  of the Noun.
        /// </summary>
        public EntityKind EntityKind
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the single Determiner which determines the noun.
        /// </summary>
        public Determiner Determiner
        {
            get;
            protected set;
        }


        public NounPhrase BindNounPhrase
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the single Noun which directly, in terms of reading order, specifies the current Noun instance.
        /// For example, consider the noun start "Felis Catus", the taxonomic nomenclature of the common domestic cat 
        /// by its genus and species.
        /// While both "Felis" and "Catus" are individual nouns, the first implicitelly specifies the second.
        /// Catus is the species of the genus Felis,
        /// but Felis also contains the species "Silvestris", commonly called a wildcat.
        /// </summary>
        public Noun SuperTaxonomicNoun
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the single Noun which this Noun directly, in terms of reading order, specifies.
        /// </summary>
        public Noun SubTaxonomicNoun
        {
            get;
            set;
        }

        #endregion

        #region Fields
        private ICollection<IDescriptor> _describedBy = new List<IDescriptor>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<IPronoun> _boundPronouns = new List<IPronoun>();
        #endregion


    }
}
