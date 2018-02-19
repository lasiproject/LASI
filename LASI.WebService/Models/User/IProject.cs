using System.Collections.Generic;

namespace LASI.WebService.Models
{
    public interface IProject
    {
        IEnumerable<UserDocument> Documents { get; }
    }
}