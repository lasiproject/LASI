// ReSharper disable All
namespace LASI.Core.Heuristics.WordNet
{
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Noun Synsets can relate to one another.
    /// http://wordnet.princeton.edu/
    /// </summary>
    enum NounLink : byte
    {
        /// <summary>
        /// The presence of this value indicates that an inter set relationship was improperly mapped from a WordNet data file.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,
        // !
        /// <summary>
        /// HypERnym
        /// </summary>
        Hypernym,
        // @
        /// <summary>
        /// InstanceHypERnym
        /// </summary>
        InstanceHypernym,
        // @i
        /// <summary>
        /// HypOnym
        /// </summary>
        Hyponym,
        //~
        /// <summary>
        /// InstanceHypOnym
        /// </summary>
        InstanceHyponym,
        // ~i
        /// <summary>
        /// MemberHolonym
        /// </summary>
        MemberHolonym,
        // #m
        /// <summary>
        /// SubstanceHolonym
        /// </summary>
        SubstanceHolonym,
        // #s
        /// <summary>
        /// PartHolonym
        /// </summary>
        PartHolonym,
        // #v
        /// <summary>
        /// MemberMeronym
        /// </summary>
        MemberMeronym,
        // %m
        /// <summary>
        /// SubstanceMeronym
        /// </summary>
        SubstanceMeronym,
        // %s
        /// <summary>
        /// PartMeronym
        /// </summary>
        PartMeronym,
        // %v
        /// <summary>
        /// Attribute
        /// </summary>
        Attribute,
        // =
        /// <summary>
        /// DerivationallyRelatedForm
        /// </summary>
        DerivationallyRelatedForm,
        // +
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,
        // ;c
        /// <summary>
        /// MemberOfThisDomain_TOPIC
        /// </summary>
        MemberOfThisDomain_TOPIC,
        // -c
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,
        //;r
        /// <summary>
        /// MemberOfThisDomain_REGION
        /// </summary>
        MemberOfThisDomain_REGION,
        // -r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,
        // ;u
        /// <summary>
        /// MemberOfThisDomain_USAGE
        /// </summary>
        MemberOfThisDomain_USAGE,
        // -u
    }

    /// <summary>
    /// Defines the different kinds of pointer relationships on which Verb Synsets can relate to one another.
    /// </summary>
    enum VerbLink : byte
    {
        /// <summary>
        /// UNDEFINED. The presence of this value indicates that an inter set relationship was improperly mapped from a WordNet data file.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,
        // !
        /// <summary>
        /// Hypernym
        /// </summary>
        Hypernym,
        // @
        /// <summary>
        /// Hyponym
        /// </summary>
        Hyponym,
        // ~
        /// <summary>
        /// Entailment
        /// </summary>
        Entailment,
        // *
        /// <summary>
        /// Cause
        /// </summary>
        Cause,
        // >
        /// <summary>
        /// AlsoSee
        /// </summary>
        AlsoSee,
        // ^
        /// <summary>
        /// Verb_Group
        /// </summary>
        Verb_Group,
        // $
        /// <summary>
        /// DerivationallyRelatedForm
        /// </summary>
        DerivationallyRelatedForm,
        // +
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,
        // ;c
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,
        // ;r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,
        // ;u
    }

    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adjective Synsets can relate to one another.
    /// </summary>
    enum AdjectiveLink : byte
    {
        /// <summary>
        /// UNDEFINED. The presence of this value indicates that an inter set relationship was improperly mapped from a WordNet data file.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,
        // !
        /// <summary>
        /// SimilarTo
        /// </summary>
        SimilarTo,
        // &
        /// <summary>
        /// ParticipleOfVerb
        /// </summary>
        ParticipleOfVerb,
        // <
        /// <summary>
        /// Pertainym_pertains_to_noun
        /// </summary>
        Pertainym_pertains_to_noun,
        // \                 Yes that really is a backslash
        /// <summary>
        /// Attribute
        /// </summary>
        Attribute,
        // =
        /// <summary>
        /// AlsoSee
        /// </summary>
        AlsoSee,
        // ^
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,
        // ;c
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,
        // ;r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,
        // ;u
    }

    /// <summary>
    /// Defines the different kinds of pointer relationships on which Adverb Synsets can relate to one another.
    /// </summary>
    enum AdverbLink : byte
    {
        /// <summary>
        /// UNDEFINED. The presence of this value indicates that an inter set relationship was improperly mapped from a WordNet data file.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Antonym
        /// </summary>
        Antonym,
        // !
        /// <summary>
        /// DerivedFromAdjective
        /// </summary>
        DerivedFromAdjective,
        // \                       Yes that really is a backslash
        /// <summary>
        /// DomainOfSynset_TOPIC
        /// </summary>
        DomainOfSynset_TOPIC,
        // ;c
        /// <summary>
        /// DomainOfSynset_REGION
        /// </summary>
        DomainOfSynset_REGION,
        // ;r
        /// <summary>
        /// DomainOfSynset_USAGE
        /// </summary>
        DomainOfSynset_USAGE,
        // ;u
    }
}
