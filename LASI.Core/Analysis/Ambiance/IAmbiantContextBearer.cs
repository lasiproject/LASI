namespace LASI.Core.Heuristics.Ambiance
{
    /// <summary>
    /// Describes an object which can be associated with an <see cref="IAmbiantContext"/>. 
    /// </summary>
    /// <remarks> This interface should only be implemented explicitly. </remarks>
    internal interface IAmbiantContextBearer
    {
        /// <summary>
        /// Imbues the <see cref="IAmbiantContextBearer"/> with the specified ambient context. 
        /// </summary>
        /// <param name="context"> The <see cref="IAmbiantContext"/> the <see cref="IAmbiantContextBearer"/> will be imbued with. </param>
        void Imbue(IAmbiantContext context);

        /// <summary>
        /// The <see cref="IAmbiantContext"/> born by the bearer. 
        /// </summary>
        IAmbiantContext Context { get; }
    }
}