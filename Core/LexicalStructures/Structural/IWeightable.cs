namespace LASI.Core
{
    public interface IWeightable
    {
        /// <summary>
        /// Gets or sets the numeric Weight of the Lexical element construct within its document.
        /// </summary>
        double MetaWeight { get; set; }
        /// <summary>
        /// Gets or sets the numeric Weight of the Lexical element construct over the context of some subset of project extant documents.
        /// </summary>
        double Weight { get; set; }
    }
}