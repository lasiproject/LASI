
namespace LASI.App
{
    /// <summary>
    /// Represents the various kinds of datasets that a Chart can Display.
    /// </summary>
    public enum ChartContentType
    {
        /// <summary>
        /// A chart of a data set's Subject Verb relationships.
        /// </summary>
        SubjectVerb,
        /// <summary>
        /// A chart of a data set's Subject Verb Object relationships.
        /// </summary>
        SubjectVerbObject,
        /// <summary>
        /// A chart of a data set's Significant NounPhrases.
        /// </summary>
        NounPhrasesOnly,
    }
}
