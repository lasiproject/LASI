using LASI.WebApp.Controllers.Controllers;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Xunit;
using MongoDB.Bson;
using LASI.WebApp.Tests.TestAttributes;
using LASI.WebApp.Tests.TestSetup;

namespace LASI.WebApp.Tests.ControllerTests
{
    public class UserDocumentsControllerTests : ControllerTestsBase
    {
        [Fact]
        [PreconfigureLASI]
        public void GetTest1()
        {
            var provider = IocContainerConfigurator.CreateConfiguredServiceCollection(User)
                .BuildServiceProvider();

            UserDocumentsController controller = provider.GetService<UserDocumentsController>();
            controller.ActionContext = provider.GetService<ActionContext>();

            var allDocuments = controller.Get();
            Assert.NotNull(allDocuments);
            Assert.NotEmpty(allDocuments);
            Assert.All(allDocuments, document => Assert.StrictEqual(ObjectId.Parse(document.UserId), User._id));
        }

        [Fact]
        [PreconfigureLASI]
        public void GetTest2()
        {
            var provider = IocContainerConfigurator.CreateConfiguredServiceCollection(User)
                .BuildServiceProvider();
            UserDocumentsController controller = provider.GetService<UserDocumentsController>();
            controller.ActionContext = provider.GetService<ActionContext>();
        }
    }
}