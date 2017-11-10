using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LASI.WebService.Api
{
    [Route("api/[Controller]/{id?}")]
    public class ValuesController : Controller
    {
        public ValuesController(IEnumerable<(string key, int value)> values) => this.values = values;

        public IEnumerable<(string key, int value)> Get() => values.ToList();

        private readonly IEnumerable<(string key, int value)> values;
    }
}
