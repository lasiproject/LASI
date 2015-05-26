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
            this.workItemsService = userWorkItemsService;
        }

        [HttpGet]
        public IEnumerable<WorkItem> Get() => workItemsService.GetAllWorkItemsForUser(User.GetUserId());

        [HttpPost("api/Tasks/Reset/{userId}")]
        public void Reset(string userId)
        {
            if (workItemsService is UserWorkItemsService) { ((UserWorkItemsService)workItemsService).RemoveAllForUser(userId); }
            else if (workItemsService is DummyUserWorkItemService)
            {
                ((DummyUserWorkItemService)workItemsService).Reset();
            }
        }
        private readonly IWorkItemsService workItemsService;
    }
}