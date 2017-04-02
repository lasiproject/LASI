namespace LASI.Core
{
    /// <summary>
    /// A specialization of the Adjective class, ComparativeAdjective represents adjectives such as "greener" and "better".
    /// </summary>
    public class ComparativeAdjective : Adjective
    {
        /// <summary>
        /// Initializes a new instance of the ComparativeAdjective class
        /// </summary>
        /// <param name="text">The text content of the Adjective.</param>
        public ComparativeAdjective(string text) : base(text) { }
    }
}