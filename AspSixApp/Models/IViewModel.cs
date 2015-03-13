namespace AspSixApp.Models
{
    public interface IViewModel
    {
        int Id { get; }
        Style Style { get; }
        string ContextmenuId { get; }
    }
}