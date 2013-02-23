/// <summary>
/// Used to specify the tagging options for the SharpNLP tagger.
/// </summary>
/// <see cref="SharpNLPTagger"/>
public enum TaggingOption
{
    /// <summary>
    /// Assign Part Of Speech Tag to each input token.
    /// </summary>
    TagIndividual,
    /// <summary>
    /// Assign tags to individual tokens and aggregate them into phrase tagged groups.
    /// </summary>
    TagAndAggregate,
    ExperimentalClauseNesting,
    GenderFind,
    NameFind,
}