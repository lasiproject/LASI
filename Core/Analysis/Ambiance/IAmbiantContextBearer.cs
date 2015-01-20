namespace LASI.Core.Analysis.Ambiance
{
    /// <summary>
    /// Describes an object which can be associated with an <see cref="IAmbiantContext"/>.
    /// </summary>
    /// <remarks>
    /// This interface should only be implemented explicitely.
    /// </remarks>
    internal interface IAmbiantContextBearer
    {
        /// <summary>
        /// Gets or sets the <see cref="IAmbiantContext"/> born by the beaer.
        /// </summary>
        IAmbiantContext Context { get; set; }
    }
}
