namespace AspSixApp.Models.DocumentStructures
{
    public abstract class TextualModel<T, TParent> : IHierarchicaViewModel<T, TParent> where TParent : IViewModel
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
        /// Contains a detailed textual representation of the model. 
        /// If not overridden by a derived class, the value will be the same as the value of <see cref="Text"/>.
        /// </summary>
        public virtual string DetailText => Text;
        public abstract Style Style { get; }

        private static int IdGenerator;
        public abstract TParent Parent { get; }
        public virtual string ContextmenuId => Parent.ContextmenuId;
    }
}