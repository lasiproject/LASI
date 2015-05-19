using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers.Controllers
{
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        public TasksController(IWorkItemsService userWorkItemsService)
        {
            this.userWorkItemsService = userWorkItemsService;
        }

        [HttpGet]
        public IEnumerable<WorkItem> Get() => userWorkItemsService.GetAllWorkItemsForUser(User.GetUserId());

        private readonly IWorkItemsService userWorkItemsService;
    }
}