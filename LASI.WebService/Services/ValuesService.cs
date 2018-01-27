using System.Collections.Generic;

namespace LASI.WebService.Services
{
    public class ValuesService : IValuesService
    {
        public ValuesService(IEnumerable<(string key, int value)> values) => Values = values;

        public IEnumerable<(string key, int value)> Values { get; }
    }
}