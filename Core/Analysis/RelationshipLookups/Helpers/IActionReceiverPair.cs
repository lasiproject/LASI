namespace LASI.Core.Analysis.Relationships
{
    public interface IActionReceiverPair<in TVerbal, out TEntity>
        where TVerbal : IVerbal
        where TEntity : IEntity
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}