using Microsoft.AspNet.Mvc;
using Xunit;
using MongoDB.Bson;
using LASI.WebApp.Tests.TestSetup;
using LASI.WebApp.Controllers;
using LASI.WebApp.Tests.TestAttributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using LASI.WebApp.Models;
using System;
using Microsoft.AspNet.Mvc.Filters;
using System.Linq;
using System.Collections.Generic;

namespace LASI.WebApp.Tests.ControllerTests
{
    public class UserDocumentsControllerTests : ControllerTestsBase
    {
        [Fact]
        public void GetTest1()
        {
            var provider = ServiceCollectionHelper.CreateConfiguredServiceCollection(User).BuildServiceProvider();

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
            var provider = ServiceCollectionHelper.CreateConfiguredServiceCollection(User).BuildServiceProvider();

            UserDocumentsController controller = provider.GetService<UserDocumentsController>();
            controller.ActionContext = provider.GetService<ActionContext>();
        }

        [Fact]
        public async Task UnauthenticatedGetReturns401()
        {
            var provider = ServiceCollectionHelper.CreateConfiguredServiceCollection(User).BuildServiceProvider();

            UserDocumentsController controller = provider.GetService<UserDocumentsController>();
            controller.ActionContext = provider.GetService<ActionContext>();
            //await controller.HttpContext.Authentication.SignOutAsync("Microsoft.AspNet.Identity.Application");
            //await provider.GetService<SignInManager<ApplicationUser>>().SignOutAsync();
            var schems = controller.HttpContext.Authentication.GetAuthenticationSchemes();
            await controller.OnActionExecutionAsync(
                context: new ActionExecutingContext(
                    actionContext: controller.ActionContext,
                    filters: (provider.GetServices<IFilterMetadata>() ?? new IFilterMetadata[0]).ToArray(),
                    actionArguments: controller.RouteData?.DataTokens ?? new Dictionary<string, object>(),
                    controller: controller), next: () =>
                    {
                        Assert.Equal(401, controller.Response.StatusCode);
                        return Task.FromResult(
                           new ActionExecutedContext(
                               controller.ActionContext,
                                (provider.GetServices<IFilterMetadata>() ?? new IFilterMetadata[0]).ToArray(),
                               controller
                           )
                       );
                    });
            var response = controller.Get();
            //Assert.Throws<Exception>(() =>
            //{
            //    var documents = controller.Get();
            //});
        }
    }
}