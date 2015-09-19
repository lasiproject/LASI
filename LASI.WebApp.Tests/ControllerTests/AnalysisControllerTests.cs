using System.Collections.Generic;
using LASI.WebApp.Controllers;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using LASI.WebApp.Tests.TestSetup;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Xunit;
using LASI.WebApp.Models.DocumentStructures;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using LASI.Utilities;
using System.Linq;

namespace LASI.WebApp.Tests.ControllerTests
{
    /// <summary>
    /// Analysis Controller Tests.
    /// </summary>
    public class AnalysisControllerTests : ControllerTestsBase
    {
        public AnalysisControllerTests()
        {
            SetupTestContext();
        }
        [Fact]
        public void GetWithWithDocumentIdReturnsCorrespondingDocument()
        {

            var documentsService = provider.GetService<IDocumentAccessor<UserDocument>>();

            foreach (var document in documentsService.GetAllForUser(User.Id))
            {
                var result = controller.Get(document.Id).Result;
                Assert.Equal(result.Id, document.Id);
            }
        }
        [Fact]
        public void GetWithWithInvalidIdReturns404()
        {
            var invalidId = "this is not a valid document id";
            var result = controller.Get(invalidId).Result;
            Assert.Equal(controller.Response.StatusCode, 404);

        }

        [Fact]
        public void GetWithNoArgumentsReturnsAllResults()
        {
            var allUserDocuments = provider.GetService<IDocumentAccessor<UserDocument>>().GetAllForUser(User.Id);
            IEnumerable<DocumentModel> results = controller.Get().Result;
            Assert.False(allUserDocuments.ExceptBy(results, e => new { e.Id, e.UserId }, e => new { e.Id, UserId = User.Id }).Any());
        }
        private System.IServiceProvider provider;
        private AnalysisController controller;
        private void SetupTestContext()
        {
            provider = ServiceCollectionHelper.CreateConfiguredServiceCollection(User)
                            .AddMockWorkItemsService(User)
                            .BuildServiceProvider();

            controller = provider.GetService<AnalysisController>();
            controller.ActionContext = provider.GetService<ActionContext>();
        }
    }
}
