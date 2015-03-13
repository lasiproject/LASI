using System;
using System.Threading.Tasks;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace AspSixApp.ViewComponents
{
    public class OrganizationManagementViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationManagementViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await Task.FromResult(0));
        }
    }
}