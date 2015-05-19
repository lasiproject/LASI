using System.Threading.Tasks;

namespace LASI.WebApp.Models
{
    public interface IUserDocument : Content.IRawTextSource
    {
        string DateUploaded { get; }
        string Id { get; }
        string UserId { get; }
    }
}