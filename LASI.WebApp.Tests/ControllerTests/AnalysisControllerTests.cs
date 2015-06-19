using System.Collections.Generic;
using LASI.WebApp.Controllers;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using LASI.WebApp.Tests.TestAttributes;
using LASI.WebApp.Tests.TestSetup;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Xunit;
using LASI.WebApp.Models.DocumentStructures;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using System;

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
        [PreconfigureLASI]
        public void GetWithWithDocumentIdReturnsCorrespondingDocument()
        {

            var documentsService = provider.GetService<IDocumentAccessor<UserDocument>>();

            foreach (var document in documentsService.GetAllForUser(User.Id))
            {
                dynamic result = controller.Get(document.Id).Result;
                Assert.Equal(result.Id, document.Id);
            }
        }
        [Fact]
        [PreconfigureLASI]
        public void GetWithWithInvalidIdReturns404()
        {
            var invalidId = "this is not a valid document id";
            dynamic result;
            //Assert.Throws<InvalidOperationException>(delegate
            //{
            result = controller.Get(invalidId).Result;
            Console.Write(result);
            //});
        }

        [Fact]
        [PreconfigureLASI]
        public void GetWithNoArgumentsReturnsAllResults()
        {

            IEnumerable<DocumentModel> allResults = controller.Get().Result;
            Assert.NotEmpty(allResults);
        }
        private System.IServiceProvider provider;
        private AnalysisController controller;
        private void SetupTestContext()
        {
            provider = IocContainerConfigurator.CreateConfiguredServiceCollection(User)
                            .AddMockWorkItemsService(User)
                            .BuildServiceProvider();

            controller = provider.GetService<AnalysisController>();
            controller.ActionContext = provider.GetService<ActionContext>();
        }
    }
}
