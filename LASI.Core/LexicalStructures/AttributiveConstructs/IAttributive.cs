namespace LASI.Core
{
    public interface IAttributive<out TAttributable> : ILexical
    {
        TAttributable AttributedTo { get; }
    }
}
