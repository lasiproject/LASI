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
        Hypernym,//@  
        InstanceHypernym,//@i 
        Hyponym,//~ 
        InstanceHyponym,//~i
        Memberholonym,//#m 
        Substanceholonym,//#subject 
        Partholonym,//#verbPhrase 
        Membermeronym,//%m 
        Substancemeronym,//%subject 
        Partmeronym,//%verbPhrase 
        Attribute,//=  
        Derivationallyrelatedform,//+  
        Domainofsynset_TOPIC,//;c 
        Memberofthisdomain_TOPIC,//-c 
        Domainofsynset_REGION,//;r 
        Memberofthisdomain_REGION,//-r 
        Domainofsynset_USAGE,//;u 
        Memberofthisdomain_USAGE,//-u  
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
        Derivationallyrelatedform,//+    
        Domainofsynset_TOPIC,//;c  
        Domainofsynset_REGION,//;r
        Domainofsynset_USAGE,//;u 
    }
}
