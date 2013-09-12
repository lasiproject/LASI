namespace LASI.Algorithm.LexicalLookup
{
    /// <summary>
    /// Defines the broad lexical categories assigned to Nouns in the WordNet system.
    /// </summary>
    enum NounCategory : byte
    {
        /// <summary>
        /// Tops
        /// </summary>
        Tops = 3,
        /// <summary>
        /// Act
        /// </summary>
        Act,
        /// <summary>
        /// Animal
        /// </summary>
        Animal,
        /// <summary>
        /// Artifact
        /// </summary>
        Artifact,
        /// <summary>
        /// Attribute
        /// </summary>
        Attribute,
        /// <summary>
        /// Body
        /// </summary>
        Body,
        /// <summary>
        /// Cognition
        /// </summary>
        Cognition,
        /// <summary>
        /// Communication
        /// </summary>
        Communication,
        /// <summary>
        /// Event
        /// </summary>
        Event,
        /// <summary>
        /// Feeling
        /// </summary>
        Feeling,
        /// <summary>
        /// Food
        /// </summary>
        Food,
        /// <summary>
        /// Group
        /// </summary>
        Group,
        /// <summary>
        /// Location
        /// </summary>
        Location,
        /// <summary>
        /// Motive
        /// </summary>
        Motive,
        /// <summary>
        /// Object
        /// </summary>
        Object,
        /// <summary>
        /// Person
        /// </summary>
        Person,
        /// <summary>
        /// Phenomenon
        /// </summary>
        Phenomenon,
        /// <summary>
        /// Plant
        /// </summary>
        Plant,
        /// <summary>
        /// Possession
        /// </summary>
        Possession,
        /// <summary>
        /// Process
        /// </summary>
        Process,
        /// <summary>
        /// Quantity
        /// </summary>
        Quantity,
        /// <summary>
        /// Relation
        /// </summary>
        Relation,
        /// <summary>
        /// Shape
        /// </summary>
        Shape,
        /// <summary>
        /// State
        /// </summary>
        State,
        /// <summary>
        /// Substance
        /// </summary>
        Substance,
        /// <summary>
        /// Time
        /// </summary>
        Time,

    }
    /// <summary>
    /// Defines the broad lexical categories assigned to Verbs in the WordNet system.
    /// </summary>
    enum VerbCategory : byte
    {
        /// <summary>
        /// Body
        /// </summary>
        Body = 29,
        /// <summary>
        /// Cognition
        /// </summary>
        Cognition,
        /// <summary>
        /// Communication
        /// </summary>
        Communication,
        /// <summary>
        /// Competition
        /// </summary>
        Competition,
        /// <summary>
        /// Consumption
        /// </summary>
        Consumption,
        /// <summary>
        /// Contact
        /// </summary>
        Contact,
        /// <summary>
        /// Creation
        /// </summary>
        Creation,
        /// <summary>
        /// Emotion
        /// </summary>
        Emotion,
        /// <summary>
        /// Motion
        /// </summary>
        Motion,
        /// <summary>
        /// Perception
        /// </summary>
        Perception,
        /// <summary>
        /// Possession
        /// </summary>
        Possession,
        /// <summary>
        /// Social
        /// </summary>
        Social,
        /// <summary>
        /// Stative
        /// </summary>
        Stative,
        /// <summary>
        /// Weather
        /// </summary>
        Weather,

    }
    /// <summary>
    /// Defines the broad lexical categories assigned to Adjectives in the WordNet system.
    /// </summary>
    enum AdjectiveCategory : byte
    {
        /// <summary>
        /// all adjective clusters
        /// </summary>
        All = 0,
        /// <summary>
        /// relational adjectives (pertainyms)
        /// </summary>
        Pert = 1,
        /// <summary>
        /// participial adjectives
        /// </summary>
        PPL = 44,
    }
    /// <summary>
    /// Defines the broad lexical categories assigned to Adverbs in the WordNet system.
    /// </summary>
    enum AdverbCategory : byte
    {
        /// <summary>
        /// All adverbs have the same category. This value is simply included for completeness.
        /// </summary>
        All = 2
    }

    /// <summary>
    /// Defines the different kinds of pointer relationships on which Noun Synsets can relate to one another.
    /// </summary>
    enum NounSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,// !  
        /// <summary>
        /// HypERnym
        /// </summary>
        HypERnym,// @  
        /// <summary>
        /// InstanceHypERnym
        /// </summary>
        InstanceHypERnym,// @i 
        /// <summary>
        /// HypOnym
        /// </summary>
        HypOnym,//~ 
        /// <summary>
        /// InstanceHypOnym
        /// </summary>
        InstanceHypOnym,// ~i
        /// <summary>
        /// MemberHolonym
        /// </summary>
        MemberHolonym,// #m 
        /// <summary>
        /// SubstanceHolonym
        /// </summary>
        SubstanceHolonym,// #s
        /// <summary>
        /// PartHolonym
        /// </summary>
        PartHolonym,// #v
        /// <summary>
        /// MemberMeronym
        /// </summary>
        MemberMeronym,// %m 
        /// <summary>
        /// SubstanceMeronym
        /// </summary>
        SubstanceMeronym,// %s
        /// <summary>
        /// PartMeronym
        /// </summary>
        PartMeronym,// %v
        /// <summary>
        /// Attribute
        /// </summary>
        Attribute,// =  
        /// <summary>
        /// DerivationallyRelatedForm
        /// </summary>
        DerivationallyRelatedForm,// +  
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,// ;c 
        /// <summary>
        /// MemberOfThisDomain_TOPIC
        /// </summary>
        MemberOfThisDomain_TOPIC,// -c 
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,//;r 
        /// <summary>
        /// MemberOfThisDomain_REGION
        /// </summary>
        MemberOfThisDomain_REGION,// -r 
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,// ;u 
        /// <summary>
        /// MemberOfThisDomain_USAGE
        /// </summary>
        MemberOfThisDomain_USAGE,// -u  
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Verb Synsets can relate to one another.
    /// </summary>
    enum VerbSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,// !
        /// <summary>
        /// Hypernym
        /// </summary>
        Hypernym,// @
        /// <summary>
        /// Hyponym
        /// </summary>
        Hyponym,// ~ 
        /// <summary>
        /// Entailment
        /// </summary>
        Entailment,// *
        /// <summary>
        /// Cause
        /// </summary>
        Cause,// >
        /// <summary>
        /// AlsoSee
        /// </summary>
        AlsoSee,// ^
        /// <summary>
        /// Verb_Group
        /// </summary>
        Verb_Group,// $    
        /// <summary>
        /// DerivationallyRelatedForm
        /// </summary>
        DerivationallyRelatedForm,// +    
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,// ;c  
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,// ;r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,// ;u 
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adjective Synsets can relate to one another.
    /// </summary>
    enum AdjectiveSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,// !
        /// <summary>
        /// SimilarTo
        /// </summary>
        SimilarTo,// &
        /// <summary>
        /// ParticipleOfVerb
        /// </summary>
        ParticipleOfVerb,// <
        /// <summary>
        /// Pertainym_pertains_to_noun
        /// </summary>
        Pertainym_pertains_to_noun,// \                 Yes that really is a backslash
        /// <summary>
        /// Attribute
        /// </summary>
        Attribute,// =
        /// <summary>
        /// AlsoSee
        /// </summary>
        AlsoSee,// ^
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,// ;c
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,// ;r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,// ;u 
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adverb Synsets can relate to one another.
    /// </summary>
    enum AdverbSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,// !    
        /// <summary>
        /// DerivedFromAdjective
        /// </summary>
        DerivedFromAdjective,// \                       Yes that really is a backslash
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,// ;c
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,// ;r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,// ;u
    }
}
