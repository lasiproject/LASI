namespace LASI.Algorithm.Lookup
{

    /// <summary>
    /// Defines the broad lexical categories assigned to Nouns in the WordNet system.
    /// </summary>
    public enum NounCategory : byte
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
    public enum VerbCategory : byte
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
    public enum AdjectiveCategory : byte
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
    public enum AdverbCategory : byte
    {
        /// <summary>
        /// All adverbs have the same category. This value is simply included for completeness.
        /// </summary>
        All = 2
    }

    /// <summary>
    /// Defines the different kinds of pointer relationships on which Noun Synsets can relate to one another.
    /// </summary>
    public enum NounSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// !
        /// </summary>
        Antonym,// !  
        /// <summary>
        /// @
        /// </summary>
        HypERnym,// @  
        /// <summary>
        /// @i
        /// </summary>
        InstanceHypERnym,// @i 
        /// <summary>
        /// ~
        /// </summary>
        HypOnym,//~ 
        /// <summary>
        /// ~i
        /// </summary>
        InstanceHypOnym,// ~i
        /// <summary>
        /// #m
        /// </summary>
        MemberHolonym,// #m 
        /// <summary>
        /// #s
        /// </summary>
        SubstanceHolonym,// #s
        /// <summary>
        /// #v
        /// </summary>
        PartHolonym,// #v
        /// <summary>
        /// %m
        /// </summary>
        MemberMeronym,// %m 
        /// <summary>
        /// %s
        /// </summary>
        SubstanceMeronym,// %s
        /// <summary>
        /// %v
        /// </summary>
        PartMeronym,// %v
        /// <summary>
        /// =
        /// </summary>
        Attribute,// =  
        /// <summary>
        /// +
        /// </summary>
        DerivationallyRelatedForm,// +  
        /// <summary>
        /// ;c
        /// </summary>
        DomainOfSynset_TOPIC,// ;c 
        /// <summary>
        /// -c
        /// </summary>
        MemberOfThisDomain_TOPIC,// -c 
        /// <summary>
        /// ;r
        /// </summary>
        DomainOfSynset_REGION,//;r 
        /// <summary>
        /// -r
        /// </summary>
        MemberOfThisDomain_REGION,// -r 
        /// <summary>
        /// ;u
        /// </summary>
        DomainOfSynset_USAGE,// ;u 
        /// <summary>
        /// -u
        /// </summary>
        MemberOfThisDomain_USAGE,// -u  
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Verb Synsets can relate to one another.
    /// </summary>
    public enum VerbSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// !
        /// </summary>
        Antonym,// !
        /// <summary>
        /// @
        /// </summary>
        Hypernym,// @
        /// <summary>
        /// ~
        /// </summary>
        Hyponym,// ~ 
        /// <summary>
        /// *
        /// </summary>
        Entailment,// *
        /// <summary>
        /// >
        /// </summary>
        Cause,// >
        /// <summary>
        /// ^
        /// </summary>
        AlsoSee,// ^
        /// <summary>
        /// $
        /// </summary>
        Verb_Group,// $    
        /// <summary>
        /// +
        /// </summary>
        DerivationallyRelatedForm,// +    
        /// <summary>
        /// ;c
        /// </summary>
        DomainOfSynset_TOPIC,// ;c  
        /// <summary>
        /// ;r
        /// </summary>
        DomainOfSynset_REGION,// ;r
        /// <summary>
        /// ;u
        /// </summary>
        DomainOfSynset_USAGE,// ;u 
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adjective Synsets can relate to one another.
    /// </summary>
    public enum AdjectiveSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// !
        /// </summary>
        Antonym,// !
        /// <summary>
        /// &
        /// </summary>
        SimilarTo,// &
        /// <summary>
        /// leftAngleBracket
        /// </summary>
        ParticipleOfVerb,// <
        /// <summary>
        /// \
        /// </summary>
        Pertainym_pertains_to_noun,// \                 Yes that really is a backslash
        /// <summary>
        /// =
        /// </summary>
        Attribute,// =
        /// <summary>
        /// ^
        /// </summary>
        AlsoSee,// ^
        /// <summary>
        /// ;c
        /// </summary>
        DomainOfSynset_TOPIC,// ;c
        /// <summary>
        /// ;r
        /// </summary>
        DomainOfSynset_REGION,// ;r
        /// <summary>
        /// ;u
        /// </summary>
        DomainOfSynset_USAGE,// ;u 
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adverb Synsets can relate to one another.
    /// </summary>
    public enum AdverbSetRelationship : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// !
        /// </summary>
        Antonym,// !    
        /// <summary>
        /// \
        /// </summary>
        DerivedFromAdjective,// \                       Yes that really is a backslash
        /// <summary>
        /// ;c
        /// </summary>
        DomainOfSynset_TOPIC,// ;c
        /// <summary>
        /// ;r
        /// </summary>
        DomainOfSynset_REGION,// ;r
        /// <summary>
        /// ;u
        /// </summary>
        DomainOfSynset_USAGE,// ;u
    }

}
