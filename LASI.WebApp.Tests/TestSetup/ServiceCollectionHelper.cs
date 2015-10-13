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

namespace LASI.WebApp.Tests.TestSetup
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateConfiguredServiceCollection(ApplicationUser applicationUser)
        {
            var services = new ServiceCollection();
            services.AddSingleton(provider => applicationUser)
                    .AddSingleton<HttpContext>(provider => new DefaultHttpContext())
                    .AddSingleton<IHttpContextAccessor>(provider => new HttpContextAccessor { HttpContext = provider.GetService<HttpContext>() })
                    .AddInMemoryStores(applicationUser)
                    .AddMvc()
                    .AddJsonOptions(json =>
                    {
                        json.SerializerSettings.Error = (s, e) => { throw e.ErrorContext.Error; };
                        json.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                        json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        json.SerializerSettings.Formatting = Formatting.Indented;
                    })
                    .AddControllersAsServices(new[] { typeof(LASI.WebApp.Startup).Assembly });

            services.AddIdentity<ApplicationUser, UserRole>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddUserStore<CustomUserStore<UserRole>>()
                    .AddRoleManager<RoleManager<UserRole>>()
                    .AddRoleStore<CustomUserStore<UserRole>>();
            services.AddSingleton<ILoggerFactory>(provider => new LoggerFactory().AddConsole(LogLevel.Critical));
            services.AddLogging()
                    .AddSingleton<ILogger<UserManager<ApplicationUser>>>(provider => new Logger<UserManager<ApplicationUser>>(provider.GetService<ILoggerFactory>()))
                    .AddSingleton<SignInManager<ApplicationUser>>(provider => new SignInManager<ApplicationUser>(
                            provider.GetService<UserManager<ApplicationUser>>(),
                            provider.GetService<IHttpContextAccessor>(),
                            provider.GetService<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                            provider.GetService<IOptions<IdentityOptions>>(),
                            provider.GetService<ILogger<SignInManager<ApplicationUser>>>()))
                    .AddTransient<ActionContext>(provider =>
                    {
                        var identityOptions = provider.GetService<IOptions<IdentityOptions>>();
                        var userManager = provider.GetService<UserManager<ApplicationUser>>();
                        var roleManager = provider.GetService<RoleManager<UserRole>>();
                        var userClaimsPrincipalFactory = new UserClaimsPrincipalFactory<ApplicationUser, UserRole>(
                            userManager,
                            roleManager,
                            identityOptions
                        );
                        var user = provider.GetService<ApplicationUser>();
                        var userClaimsPrincipal = userClaimsPrincipalFactory.CreateAsync(user);
                        var httpContext = provider.GetService<HttpContext>();
                        var mockUserClaimsPrincipleFactory = new Mock<UserClaimsPrincipalFactory<ApplicationUser, UserRole>>();
                        mockUserClaimsPrincipleFactory.Setup(m => m.CreateAsync(user)).ReturnsAsync(new System.Security.Claims.ClaimsPrincipal(user.Claims.Select(c => c.Subject)));

                        httpContext.User = userClaimsPrincipal.Result;
                        return new ActionContext
                        {
                            HttpContext = httpContext
                        };
                    });
            return services;
        }
    }
}
