namespace LASI.Core.Analysis.Relationships
{
    public interface IActionReceiverPair<out TVerbal, out TEntity>
        where TVerbal : IVerbal
        where TEntity : IEntity
    {
        /// <summary>
        /// Gets the Action.
        /// </summary>
        TVerbal Action { get; }

        /// <summary>
        /// Gets the Receiver of the Action.
        /// </summary>
        TEntity Receiver { get; }
    }
}