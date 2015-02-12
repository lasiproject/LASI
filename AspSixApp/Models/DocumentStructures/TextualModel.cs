namespace AspSixApp.Models.DocumentStructures
{
    public abstract class TextualModel<T> : IViewModel<T>
    {
        public TextualModel(T modelFor)
        {
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ModelFor = modelFor;
        }
        public int Id { get; }
        public T ModelFor { get; }
        public abstract string Text { get; }
        /// <summary>
        /// Contains a detailed textual repesentaion of the model. 
        /// If not overriden by a derived class, the value will be the same as the value of <see cref="Text"/>.
        /// </summary>
        public virtual string DetailText => Text;
        public abstract Style Style { get; }
        private static int IdGenerator;
    }
}