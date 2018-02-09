namespace LASI.Core.Heuristics.Relationships
{
    public interface IActionReceiverPair<out TVerbal, out TEntity>
        where TVerbal : IVerbal
        where TEntity : IEntity
    {
        /// <summary>
        /// The Action.
        /// </summary>
        TVerbal Action { get; }

        /// <summary>
        /// The Receiver of the Action.
        /// </summary>
        TEntity Receiver { get; }
    }
}