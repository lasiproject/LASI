namespace LASI.ContentSystem.TaggerEncapsulation
{

    /// <summary>
    /// Used to specify the tagging options for the SharpNLP tagger.
    /// </summary>
    /// <see cref="SharpNLPTagger"/>
    public enum TaggerMode
    {
        /// <summary>
        /// Assign Part Of Speech Tag to each input token.
        /// </summary>
        TagIndividual,
        /// <summary>
        /// The default mode. Tags words with the form "word/tag" and simple phrases with the form [ tag word1/t1 word2/t2... ]
        /// </summary>
        TagAndAggregate,
        /// <summary>
        /// Parses and nests arbitarily
        /// </summary>
        ExperimentalClauseNesting,
        /// <summary>
        /// Embeds gender liklihood information with nouns
        /// </summary>
        GenderFind,
        /// <summary>
        /// Embeds enity recognition with nouns for broad categories such as location, organization, etc.
        /// </summary>
        NameFind,
    }
}