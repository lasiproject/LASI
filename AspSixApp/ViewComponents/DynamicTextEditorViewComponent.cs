using System;
using System.Threading.Tasks;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace AspSixApp.ViewComponents
{
    public class DynamicTextEditorViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DynamicTextEditorViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await Task.FromResult(0));
        }
    }
}