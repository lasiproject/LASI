using System;
using System.Security.Principal;
using AspSixApp.CustomIdentity;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AspSixApp.ViewComponents
{
    public class DocumentListViewComponent : ViewComponent
    {
        public DocumentListViewComponent(UserManager<ApplicationUser> userManager, IInputDocumentStore<UserDocument> documentStore)
        {
            this.userManager = userManager;
            this.documentStore = documentStore;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxResults)
        {
            var user = await userManager.FindByIdAsync(Context.User.Identity.GetUserId());
            var userDocuments =
                from document in documentStore.GetAllUserInputDocuments(user.Id)
                orderby document.DateUploaded ?? DateTime.MinValue descending
                select document.Name;
            return View(userDocuments.Take(maxResults));
        }
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IInputDocumentStore<UserDocument> documentStore;
    }
}