using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LASI.WebService.Data;

namespace LASI.WebService.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger) => (this.signInManager, this.logger) = (signInManager, logger);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger logger;
    }
}
