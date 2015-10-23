namespace LASI.Interop
{
    /// <summary>
    /// Broadly specifies the various resource usage profiles of the program. The default is <see cref="High"/>.
    /// </summary>
    /// <remarks><see cref="PerformanceProfile"/> values provided decidedly coarse grained control over the amount of resources consumed. 
    /// They do not specify limits or guarantee maximum consumption but rather serve as guidelines.
    /// </remarks>
    public enum PerformanceProfile
    {
        /// <summary>
        /// Indicates a liberal consumption of available system resources.
        /// </summary>
        High = 0,
        /// <summary>
        /// Indicates a considerable consumption of available system resources.
        /// </summary>
        Normal,
        /// <summary>
        /// Indicates a conservative consumption of available system resources.
        /// </summary>
        Low
    }
}
