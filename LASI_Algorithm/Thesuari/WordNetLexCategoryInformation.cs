namespace LASI.Algorithm.Thesauri
{

    /// <summary>
    /// Defines the broad lexical categories assigned to Nouns in the WordNet system.
    /// </summary>
    public enum WordNetNounCategory : byte
    {
        Tops = 3,
        Act,
        Animal,
        Artifact,
        Attribute,
        Body,
        Cognition,
        Communication,
        Event,
        Feeling,
        Food,
        Group,
        Location,
        Motive,
        Object,
        Person,
        Phenomenon,
        Plant,
        Possession,
        Process,
        Quantity,
        Relation,
        Shape,
        State,
        Substance,
        Time,

    }

    /// <summary>
    /// Defines the broad lexical categories assigned to Verbs in the WordNet system.
    /// </summary>
    public enum WordNetVerbCategory : byte
    {
        Body = 29,
        Cognition,
        Communication,
        Competition,
        Consumption,
        Contact,
        Creation,
        Emotion,
        Motion,
        Perception,
        Possession,
        Social,
        Stative,
        Weather,

    }

    /// <summary>
    /// Defines the broad lexical categories assigned to Adjectives in the WordNet system.
    /// </summary>
    public enum WordNetAdjectiveCategory : byte
    {
        All = 0,//	all adjective clusters
        Pert = 1,	//relational adjectives (pertainyms)
        PPL = 44,//participial adjectives
    }

    /// <summary>
    /// Defines the broad lexical categories assigned to Adverbs in the WordNet system.
    /// </summary>
    public enum WordNetAdverbCategory : byte
    {
        All = 2
    }

    /// <summary>
    /// Defines the different kinds of pointer relationships on which Noun Synsets can relate to one another.
    /// </summary>
    public enum NounPointerSymbol : byte
    {
        UNDEFINED = 0,
        Antonym,// !  
        HypERnym,// @  
        InstanceHypERnym,// @i 
        HypOnym,//~ 
        InstanceHypOnym,// ~i
        MemberHolonym,// #m 
        SubstanceHolonym,// #s
        PartHolonym,// #v
        MemberMeronym,// %m 
        SubstanceMeronym,// %s
        PartMeronym,// %v
        Attribute,// =  
        DerivationallyRelatedForm,// +  
        DomainOfSynset_TOPIC,// ;c 
        MemberOfThisDomain_TOPIC,// -c 
        DomainOfSynset_REGION,//;r 
        MemberOfThisDomain_REGION,// -r 
        DomainOfSynset_USAGE,// ;u 
        MemberOfThisDomain_USAGE,// -u  
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Verb Synsets can relate to one another.
    /// </summary>
    public enum VerbPointerSymbol : byte
    {
        UNDEFINED = 0,
        Antonym,// !
        Hypernym,// @
        Hyponym,// ~ 
        Entailment,// *
        Cause,// >
        AlsoSee,// ^
        Verb_Group,// $    
        DerivationallyRelatedForm,// +    
        DomainOfSynset_TOPIC,// ;c  
        DomainOfSynset_REGION,// ;r
        DomainOfSynset_USAGE,// ;u 
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adjective Synsets can relate to one another.
    /// </summary>
    public enum AdjectivePointerSymbol : byte
    {
        UNDEFINED = 0,
        Antonym,// !
        SimilarTo,// &
        ParticipleOfVerb,// <
        Pertainym_pertains_to_noun,// \                 Yes that really is a backslash
        Attribute,// =
        AlsoSee,// ^
        DomainOfSynset_TOPIC,// ;c
        DomainOfSynset_REGION,// ;r
        DomainOfSynset_USAGE,// ;u 
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adverb Synsets can relate to one another.
    /// </summary>
    public enum AdverbPointerSymbol : byte
    {
        UNDEFINED = 0,
        Antonym,// !    
        DerivedFromAdjective,// \                       Yes that really is a backslash
        DomainOfSynset_TOPIC,// ;c
        DomainOfSynset_REGION,// ;r
        DomainOfSynset_USAGE,// ;u
    }

}
