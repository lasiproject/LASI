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
        public TasksController([Activate]IWorkItemsService userWorkItemsService)
        {
            this.userWorkItemsService = userWorkItemsService;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkItem>> Get()
        {
            var userId = User.GetUserId();
            return await Task.FromResult(userWorkItemsService.GetAllWorkItemsForUser(userId));
        }
        private readonly IWorkItemsService userWorkItemsService;
    }
}