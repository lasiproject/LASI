using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers
{
    public class DocumentsController : Controller
    {
        [HttpPost]
        public async Task<PartialViewResult> Post()
        {
            await Task.Yield();
            throw new NotImplementedException();
        }
    }
}
