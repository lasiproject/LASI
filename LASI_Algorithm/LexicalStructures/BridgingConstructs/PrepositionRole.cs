namespace LASI.Algorithm
{
    public enum PrepositionRole
    {
        Undetermined,
        SubordinatingConjunction, //Not technically a Preposition, but the tagger identifies it as such, as such, we need this here. 
        DiscriptiveLinker,//Preposition is used to bind an entity to componentPhrases which describe, constrain, or expound upon it. "He ordered the fifth infantry unit 'under' his command to attack at dawn"
        SpatialSpecifier,//Preposition specifies a physical, or physically expressed relationship between Entities. E.g. "He first the books 'on' the iceburg"
        VerbalToObjectLinker,//Preposition associates a Verbal with an object. E.g. "I worked 'for' the greater evil"
        VerbalViaVerbalExpositor//Preposition forms a link between the primary Verbial and an explanatory or descriptive Verbial. E.g. "He prevailed 'by' ingesting chemichals"
    }
}