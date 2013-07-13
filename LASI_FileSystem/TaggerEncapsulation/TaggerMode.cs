/// <summary>
/// Used to specify the tagging options for the SharpNLP tagger.
/// </summary>
/// <see cref="SharpNLPTagger"/>

namespace SharpNatrualLanguageProcessing
{
    public enum TaggerMode
    {
        /// <summary>
        /// Assign Part Of Speech Tag to each input token.
        /// </summary>
        TagIndividual,
        /// <summary>
        /// Assign tags to individual tokens and aggregate them into tagged groups.
        /// </summary>
        TagAndAggregate,
        ExperimentalClauseNesting,
        GenderFind,
        NameFind,
    }
}