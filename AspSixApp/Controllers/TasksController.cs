using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AspSixApp.Models;
using AspSixApp.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers.Controllers
{
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        public TasksController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<UserWorkItem>> Get()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            return user.ActiceWorkItems;
        }

        private readonly UserManager<ApplicationUser> userManager;
    }
}