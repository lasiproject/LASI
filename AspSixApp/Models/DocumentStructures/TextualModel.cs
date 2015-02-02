namespace AspSixApp.Models.DocumentStructures
{
    abstract class TextualModel<T> : IViewModel<T>
	{
		public TextualModel(T modelFor) {
			Id = System.Threading.Interlocked.Increment(ref IdGenerator);
			ModelFor = modelFor;
		}
		public int Id { get; private set; }
		public T ModelFor { get; private set; }
		public abstract string Text { get; }
		public abstract Style Style { get; }
		private static int IdGenerator;
	}
}