using Microsoft.AspNet.Mvc;
using Xunit;
using MongoDB.Bson;
using LASI.WebApp.Tests.TestSetup;
using LASI.WebApp.Controllers;
using LASI.WebApp.Tests.TestAttributes;
using Microsoft.Extensions.DependencyInjection;

namespace LASI.WebApp.Tests.ControllerTests
{
    public class UserDocumentsControllerTests : ControllerTestsBase
    {
        [Fact]
        public void GetTest1()
        {
            var provider = ServiceCollectionHelper.CreateConfiguredServiceCollection(User)
                .BuildServiceProvider();

            UserDocumentsController controller = provider.GetService<UserDocumentsController>();
            controller.ActionContext = provider.GetService<ActionContext>();

            var allDocuments = controller.Get();
            Assert.NotNull(allDocuments);
            Assert.NotEmpty(allDocuments);
            Assert.All(allDocuments, document => Assert.StrictEqual(ObjectId.Parse(document.UserId), User._id));
        }

        [Fact]
        public void GetTest2()
        {
            var provider = ServiceCollectionHelper.CreateConfiguredServiceCollection(User)
                .BuildServiceProvider();

            UserDocumentsController controller = provider.GetService<UserDocumentsController>();
            controller.ActionContext = provider.GetService<ActionContext>();
        }
    }
}