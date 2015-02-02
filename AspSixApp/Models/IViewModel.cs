namespace AspSixApp.Models
{
    interface IViewModel<out T>
    {
        T ModelFor { get; }
        int Id { get; }
        Style Style { get; }
    }
}