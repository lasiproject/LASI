﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LASI.WebApp.Controllers;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using LASI.WebApp.Tests.TestAttributes;
using LASI.WebApp.Tests.TestSetup;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace LASI.WebApp.Tests.ControllerTests
{
    public class ResultsControllerTests : ControllerTestsBase
    {
        [Fact]
        [PreconfigureLASI]
        public void GetWithNoParamsTest1()
        {
            var provider = IocContainerConfigurator.CreateConfiguredServiceCollection(User)
                .AddMockWorkItemsService(User)
                .BuildServiceProvider();
            ResultsController controller = provider.GetService<ResultsController>();
            controller.ActionContext = provider.GetService<ActionContext>();

            IEnumerable<dynamic> allResults = controller.Get().Result;
            Assert.NotEmpty(allResults);
        }
    }
}
