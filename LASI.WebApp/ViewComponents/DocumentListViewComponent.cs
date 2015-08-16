using System;
using System.Security.Principal;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;
using LASI.WebApp.Models.User;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;

namespace LASI.WebApp.ViewComponents
{
    public class DocumentListViewComponent : ViewComponent
    {
        public DocumentListViewComponent(IDocumentAccessor<UserDocument> documentStore, UserManager<ApplicationUser> userManager)
        {
            this.documentStore = documentStore;
            this.userManager = userManager;
        }
        /// <summary>
        /// Retrieves the last <paramref name="maxResults"/> documents uploaded by the user ordered by date uploaded.
        /// </summary>
        /// <param name="maxResults">The maximum number of documents to retrieve.</param>
        /// <returns>The last <paramref name="maxResults"/> documents uploaded by the user ordered by date uploaded.</returns>
        public async Task<IViewComponentResult> InvokeAsync(int maxResults)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            var userDocuments =
                from document in documentStore.GetAllForUser(userId)
                let dateUploaded = (DateTimeOffset)(JToken)(document.DateUploaded)
                orderby dateUploaded descending
                select ActiveUserDocument.FromUserDocument(document);
            return await Task.FromResult(View(userDocuments.Take(maxResults).Reverse()));
        }

        private readonly IDocumentAccessor<UserDocument> documentStore;
        private readonly UserManager<ApplicationUser> userManager;
    }
}