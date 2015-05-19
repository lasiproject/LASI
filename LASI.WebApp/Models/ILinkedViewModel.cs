namespace LASI.WebApp.Models
{
    public interface ILinkedViewModel<out T> : IViewModel
    {
        T ModelFor { get; }
    }
}