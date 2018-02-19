namespace LASI.WebService.Models
{
    public interface ILinkedViewModel<out T> : IViewModel
    {
        [Newtonsoft.Json.JsonIgnore]
        T ModelFor { get; }
    }
}