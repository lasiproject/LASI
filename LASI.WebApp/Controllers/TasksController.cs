using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LASI.WebApp.Controllers
{
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        public TasksController(IWorkItemsService userWorkItemsService)
        {
            this.workItemsService = userWorkItemsService;
        }

        [HttpGet]
        public IEnumerable<WorkItem> Get()
        {
            var userId = User.GetUserId();
            return userId == null ? Enumerable.Empty<WorkItem>() : workItemsService.GetAllWorkItemsForUser(userId);
        }

        [HttpPost]
        public void Reset(string userId)
        {
            // This is terrible.
            if (workItemsService is WorkItemsService) { ((WorkItemsService)workItemsService).RemoveAllForUser(userId); }
            else if (workItemsService is DummyUserWorkItemService)
            {
                ((DummyUserWorkItemService)workItemsService).Reset();
            }
        }
        private readonly IWorkItemsService workItemsService;
    }
}