using System.Collections.Generic;

namespace LASI.WebService.Services
{
    public interface IValuesService
    {
        IEnumerable<(string key, int value)> Values { get; }
    }
}