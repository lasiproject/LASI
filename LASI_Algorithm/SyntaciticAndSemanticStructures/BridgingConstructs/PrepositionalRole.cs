namespace LASI.Algorithm
{
    public enum PrepositionalRole
    {
        DiscriptiveLinker,//Preposition is used to bind an entity to phrases which describe, constrain, or expound upon it. "He ordered the fifth infantry unit 'under' his command to attack at dawn.
        SpatialSpecifier,//Preposition specifies a physical, or physically expressed relationship between Entities. E.g. "He left the books 'on' the iceburg"
        VerbialToObjectLinker,//Preposition associates a Verbial with an object. E.g. "I worked 'for' the greater evil"
        VerbialViaVerbialExpositor//Preposition forms a link between the primary Verbial and an explanatory or descriptive Verbial. E.g. "He prevailed 'by' ingesting chemichals"
    }
}