namespace LASI.Content.Serialization
{
    using System.Collections.Concurrent;
    using ILexical = Core.ILexical;
    using Interlocked = System.Threading.Interlocked;
    class NodeNameMapper
    {
        public string this[ILexical element] =>
            element == null ? string.Empty : $"{element?.GetType().Name} {elementIds.GetOrAdd(element, e => Interlocked.Increment(ref idGenerator))}";

        private int idGenerator;
        private readonly ConcurrentDictionary<ILexical, int> elementIds = new ConcurrentDictionary<ILexical, int>();
    }
}
