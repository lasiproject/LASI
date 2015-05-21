using System.Collections.Generic;

namespace LASI.WebApp.Models
{
    public interface IProject
    {
        IEnumerable<UserDocument> Documents { get; }
    }
}