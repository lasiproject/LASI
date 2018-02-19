using Newtonsoft.Json;

namespace LASI.WebService.Models.DocumentStructures
{
    public abstract class TextualModel<T, TParent> : IHierarchicaViewModel<T, TParent> where TParent : IViewModel
    {
        public TextualModel(T modelFor)
        {
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ModelFor = modelFor;
        }

        public virtual string ContextmenuId => Parent?.ContextmenuId;

        public int Id { get; }

        [JsonIgnore]
        public T ModelFor { get; }

        [JsonIgnore]
        public abstract TParent Parent { get; }

        public abstract Style Style { get; }

        [JsonIgnore]
        public abstract string Text { get; }

        static int IdGenerator;
    }
}