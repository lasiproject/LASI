namespace LASI.Core
{
    public interface IPrepositionLinkable : ILexical
    {
        /// <summary>
        /// The <see cref="IPrepositional"/> instance lexically to the left of the element.
        /// </summary>
        IPrepositional LeftPrepositional { get; }

        /// <summary>
        /// The <see cref="IPrepositional"/> instance lexically to the right of the element.
        /// </summary>
        IPrepositional RightPrepositional { get; }

        /// <summary>
        /// Binds the specified prepositional as the leftward prepositional of the element.
        /// </summary>
        /// <param name="prepositional">The prepositional to bind.</param>
        void BindLeftPrepositional(IPrepositional prepositional);

        /// <summary>
        /// Binds the specified prepositional as the rightward prepositional of the element.
        /// </summary>
        /// <param name="prepositional">The prepositional to bind.</param>
        void BindRightPrepositional(IPrepositional prepositional);
    }
}