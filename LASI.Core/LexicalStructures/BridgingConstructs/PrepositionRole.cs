namespace LASI.Core
{
    /// <summary>
    /// Defines the various roles which preposition can play.
    /// </summary>
    public enum PrepositionRole
    {
        /// <summary>
        /// A specific role has not been determined for the preposition.
        /// </summary>
        Undetermined,
        /// <summary>
        /// Not technically a Preposition, but the tagger identifies it as such, as such, we need this here. 
        /// </summary>
        SubordinatingConjunction,
        /// <summary>
        /// Preposition is used to bind an entity to componentPhrases which describe, constrain, or expound upon it. "He ordered the fifth infantry unit 'under' his command to attack at dawn"
        /// </summary>
        DiscriptiveLinker,
        /// <summary>
        /// Preposition specifies a physical, or physically expressed relationship between Entities. E.g. "He first the books 'on' the iceberg"
        /// </summary>
        SpatialSpecifier,//
        /// <summary>
        /// Preposition associates a Verbal with an object. E.g. "I worked 'for' the greater evil"
        /// </summary>
        VerbalToObjectLinker,
        /// <summary>
        /// Preposition forms a link between the primary Verbal and an explanatory or descriptive Verbal. E.g. "He prevailed 'by' ingesting chemicals"
        /// </summary>
        VerbalViaVerbalExpositor
    }
}