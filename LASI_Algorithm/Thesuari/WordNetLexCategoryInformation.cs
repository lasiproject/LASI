namespace LASI.Algorithm.Thesauri
{
    public enum WordNetVerbCategory
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
        ARBITRARY = 100
    }
    public enum WordNetNounCategory
    {
        Entity = 3,
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
        ARBITRARY
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Noun Synsets can relate to one another.
    /// </summary>
    public enum NounPointerSymbol
    {
        UNDEFINED = 0,
        Antonym,//!  
        HypERnym,//@  
        InstanceHypERnym,//@i 
        HypOnym,//~ 
        InstanceHypOnym,//~i
        MemberHolonym,//#m 
        SubstanceHolonym,//#s
        PartHolonym,//#v
        MemberMeronym,//%m 
        SubstanceMeronym,//%s
        PartMeronym,//%v
        Attribute,//=  
        DerivationallyRelatedForm,//+  
        DomainOfSynset_TOPIC,//;c 
        MemberOfThisDomain_TOPIC,//-c 
        DomainOfSynset_REGION,//;r 
        MemberOfThisDomain_REGION,//-r 
        DomainOfSynset_USAGE,//;u 
        MemberOfThisDomain_USAGE,//-u  
    }
    /// <summary>
    /// Defines the different kinds of pointer relationships on which Verb Synsets can relate to one another.
    /// </summary>
    public enum VerbPointerSymbol
    {
        UNDEFINED = 0,
        Antonym,//!
        Hypernym,//@
        Hyponym,//~ 
        Entailment,//*
        Cause,//>
        Also_see,//^
        Verb_Group,//$    
        DerivationallyRelatedForm,//+    
        DomainOfSynset_TOPIC,//;c  
        DomainOfSynset_REGION,//;r
        DomainOfSynset_USAGE,//;u 
    }
}
