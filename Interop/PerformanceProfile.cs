namespace LASI.Interop
{
    /// <summary>
    /// Broadly specifies the various resource usage profiles of the program.
    /// </summary>
    public enum PerformanceProfile
    {
        /// <summary>
        /// High resource usage indicates a liberal allocation and consumption of available
        /// system resources.
        /// </summary>
        High = 0,

        /// <summary>
        /// Normal resource usage indicates a modest allocation and consumption of available
        /// system resources.
        /// </summary>
        Normal,

        /// <summary>
        /// High resource usage indicates a conservative allocation and consumption of available
        /// system resources.
        /// </summary>
        Low
    }
}
