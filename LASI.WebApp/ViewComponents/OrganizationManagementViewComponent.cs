using System.Threading.Tasks;
using LASI.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace LASI.WebApp.ViewComponents
{
    public class OrganizationManagementViewComponent : ViewComponent
    {
        public OrganizationManagementViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await Task.FromResult(0));

        private readonly UserManager<ApplicationUser> userManager;
    }
}