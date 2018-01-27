using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LASI.Core;
using LASI.WebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LASI.WebService.Api
{
    [Route("api/[Controller]/{id?}")]
    public class ValuesController : Controller
    {
        public ValuesController(IValuesService valuesService) => values = valuesService.Values.ToList();

        [HttpGet]
        public Task<IEnumerable<(string key, int value)>> GetAsync() => Task.FromResult(values);

        public NounPhrase Tester() => new NounPhrase(new[] { new CommonSingularNoun("tester") });

        private readonly IEnumerable<(string key, int value)> values;
    }
}
