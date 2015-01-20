namespace LASI.Core
{
    public interface IAttributable<out TAttributableAs, out TAttributedBy> : ILexical where TAttributableAs : IAttributable<TAttributableAs, TAttributedBy> where TAttributedBy : IAttributive<TAttributableAs>
    {
        System.Collections.Generic.IEnumerable<TAttributedBy> AttributedBy { get; }
    }
}