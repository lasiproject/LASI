using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LASI.WebApp.Models;
using LASI.WebApp.Persistence;
using LASI.WebApp.Tests.Mocks;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp.Tests.TestSetup
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateConfiguredServiceCollection(ApplicationUser user)
        {
            var services = new ServiceCollection();
            services.AddMvc()
                    .AddJsonOptions(json =>
                    {
                        json.SerializerSettings.Error = (s, e) => { throw e.ErrorContext.Error; };
                        json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        json.SerializerSettings.Formatting = Formatting.Indented;
                    })
                    .AddControllersAsServices(new[] { typeof(Startup).Assembly });

            services.AddIdentity<ApplicationUser, UserRole>()
                    .AddUserStore<CustomUserStore<UserRole>>()
                    .AddRoleStore<CustomUserStore<UserRole>>();

            services.AddSingleton<HttpContext, DefaultHttpContext>()
                    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                    .AddInMemoryStores(user)
                    .AddLogging()
                    .AddSingleton(provider => new LoggerFactory().AddConsole(LogLevel.Critical))
                    .AddSingleton<ILogger<UserManager<ApplicationUser>>>(provider => new Logger<UserManager<ApplicationUser>>(provider.GetService<ILoggerFactory>()))
                    .AddSingleton(provider =>
                    {
                        return new SignInManager<ApplicationUser>(
                        userManager: provider.GetService<UserManager<ApplicationUser>>(),
                        contextAccessor: provider.GetService<IHttpContextAccessor>(),
                        claimsFactory: provider.GetService<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                        optionsAccessor: provider.GetService<IOptions<IdentityOptions>>(),
                        logger: provider.GetService<ILogger<SignInManager<ApplicationUser>>>());
                    })
                    .AddScoped(provider =>
                    {
                        var identityOptions = provider.GetService<IOptions<IdentityOptions>>();
                        var userManager = provider.GetService<UserManager<ApplicationUser>>();
                        var roleManager = provider.GetService<RoleManager<UserRole>>();
                        var userClaimsPrincipalFactory = new UserClaimsPrincipalFactory<ApplicationUser, UserRole>(userManager, roleManager, identityOptions);
                        var userClaimsPrincipal = userClaimsPrincipalFactory.CreateAsync(user);
                        var httpContext = provider.GetService<HttpContext>();
                        var optionsAccessor = provider.GetService<IOptions<IdentityOptions>>();
                        var mockUserClaimsPrincipleFactory = new MockUserClaimsPrincipalFactory(userManager, roleManager, optionsAccessor);

                        httpContext.User = userClaimsPrincipal.Result;
                        return new ActionContext { HttpContext = httpContext };
                    });
            return services;
        }
    }
}