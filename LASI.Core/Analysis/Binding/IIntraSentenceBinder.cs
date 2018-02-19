namespace LASI.Core.Heuristics.Binding
{
    public interface IIntraSentenceBinder
    {
        /// <summary>
        /// Performs binding between the applicable elements within the provided <see cref="Sentence"/>.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        void Bind(Sentence sentence);
    }
}