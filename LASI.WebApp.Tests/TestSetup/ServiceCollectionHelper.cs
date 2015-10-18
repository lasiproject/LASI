using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;
using System.Linq;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Http;
using Moq;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp.Tests.TestSetup
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateConfiguredServiceCollection(ApplicationUser applicationUser) {
            var services = new ServiceCollection();
            services.AddMvc()
                    .AddJsonOptions(json => {
                        json.SerializerSettings.Error = (s, e) => { throw e.ErrorContext.Error; };
                        json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        json.SerializerSettings.Formatting = Formatting.Indented;
                    })
                    .AddControllersAsServices(new[] { typeof(Startup).Assembly });

            services.AddIdentity<ApplicationUser, UserRole>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddUserStore<CustomUserStore<UserRole>>()
                    .AddRoleManager<RoleManager<UserRole>>()
                    .AddRoleStore<CustomUserStore<UserRole>>();

            services.AddSingleton<HttpContext>(provider => new DefaultHttpContext())
                    .AddSingleton<IHttpContextAccessor>(provider => new HttpContextAccessor
                    {
                        HttpContext = provider.GetService<HttpContext>()
                    })
                    .AddInMemoryStores(applicationUser)
                    .AddLogging()
                    .AddSingleton(provider => new LoggerFactory().AddConsole(LogLevel.Critical))
                    .AddSingleton<ILogger<UserManager<ApplicationUser>>>(provider => new Logger<UserManager<ApplicationUser>>(provider.GetService<ILoggerFactory>()))
                    .AddSingleton(provider => {
                        return new SignInManager<ApplicationUser>(
                        userManager: provider.GetService<UserManager<ApplicationUser>>(),
                        contextAccessor: provider.GetService<IHttpContextAccessor>(),
                        claimsFactory: provider.GetService<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                        optionsAccessor: provider.GetService<IOptions<IdentityOptions>>(),
                        logger: provider.GetService<ILogger<SignInManager<ApplicationUser>>>());
                    })
                    .AddTransient(provider => {
                        var identityOptions = provider.GetService<IOptions<IdentityOptions>>();
                        var userManager = provider.GetService<UserManager<ApplicationUser>>();
                        var roleManager = provider.GetService<RoleManager<UserRole>>();
                        var userClaimsPrincipalFactory = new UserClaimsPrincipalFactory<ApplicationUser, UserRole>(userManager, roleManager, identityOptions);
                        var userClaimsPrincipal = userClaimsPrincipalFactory.CreateAsync(applicationUser);
                        var httpContext = provider.GetService<HttpContext>();
                        var mockUserClaimsPrincipleFactory = new Mock<UserClaimsPrincipalFactory<ApplicationUser, UserRole>>();
                        mockUserClaimsPrincipleFactory
                            .Setup(m => m.CreateAsync(applicationUser))
                            .ReturnsAsync(new ClaimsPrincipal(applicationUser.Claims.Select(claim => claim.Subject)));
                        httpContext.User = userClaimsPrincipal.Result;
                        return new ActionContext { HttpContext = httpContext };
                    });
            return services;
        }
    }
}
