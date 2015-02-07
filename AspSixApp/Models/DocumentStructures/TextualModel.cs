namespace AspSixApp.Models.DocumentStructures
{
    abstract class TextualModel<T> : IViewModel<T>
    {
        public TextualModel(T modelFor)
        {
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ModelFor = modelFor;
            DetailText = Text;
        }
        public int Id { get; }
        public T ModelFor { get; }
        public abstract string Text { get; }
        public virtual string DetailText { get; }
        public abstract Style Style { get; }
        private static int IdGenerator;
    }
}