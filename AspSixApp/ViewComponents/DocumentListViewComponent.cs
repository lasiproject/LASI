using System;
using System.Security.Principal;
using AspSixApp.CustomIdentity;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AspSixApp.Models.User;
using Newtonsoft.Json.Linq;

namespace AspSixApp.ViewComponents
{
    public class DocumentListViewComponent : ViewComponent
    {
        public DocumentListViewComponent(UserManager<ApplicationUser> userManager, IInputDocumentStore<UserDocument> documentStore)
        {
            this.userManager = userManager;
            this.documentStore = documentStore;
        }

        /// <summary>
        /// Retrieves the last <paramref name="maxResults"/> documents uploaded by the user ordered by date uploaded.
        /// </summary>
        /// <param name="maxResults">The maximum number of documents to retrieve.</param>
        /// <returns>The last <paramref name="maxResults"/> documents uploaded by the user ordered by date uploaded.</returns>
        public async Task<IViewComponentResult> InvokeAsync(int maxResults)
        {
            var user = await userManager.FindByIdAsync(Context.User.Identity.GetUserId());
            var userDocuments =
                from document in documentStore.GetAllUserInputDocuments(user.Id)
                let dateUploaded = (DateTime)(JToken)(document.DateUploaded)
                orderby dateUploaded descending
                select ActiveUserDocument.FromUserDocument(document);
            return View(userDocuments.Take(maxResults).OrderBy(document => document.DateUploaded));
        }


        private readonly UserManager<ApplicationUser> userManager;
        private readonly IInputDocumentStore<UserDocument> documentStore;
    }
}