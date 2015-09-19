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

namespace LASI.WebApp.Tests.TestSetup
{
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateConfiguredServiceCollection(ApplicationUser applicationUser)
        {
            var services = new ServiceCollection();
            services.AddSingleton(provider => applicationUser)
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
            services.AddTransient(provider =>
            {
                var user = provider.GetService<ApplicationUser>();
                var httpContext = new DefaultHttpContext();
                var contextAccessor = new HttpContextAccessor
                {
                    HttpContext = httpContext
                };
                var identityOptions = provider.GetService<IOptions<IdentityOptions>>();
                var userManager = provider.GetService<UserManager<ApplicationUser>>();
                var roleManager = provider.GetService<RoleManager<UserRole>>();
                var userClaimsPrincipalFactory = new UserClaimsPrincipalFactory<ApplicationUser, UserRole>(
                    userManager,
                    roleManager,
                    identityOptions
                );
                var userClaimsPrincipal = userClaimsPrincipalFactory.CreateAsync(user);
                var optionsAccessor = new OptionsManager<IdentityOptions>(Enumerable.Empty<IConfigureOptions<IdentityOptions>>());
                var logger = new Logger<SignInManager<ApplicationUser>>(new LoggerFactory());
                var signInManager = new SignInManager<ApplicationUser>(
                    userManager,
                    contextAccessor,
                    userClaimsPrincipalFactory,
                    optionsAccessor,
                    logger
                );
                httpContext.User = userClaimsPrincipal.Result;
                signInManager.SignInAsync(user, true);
                return new ActionContext
                {
                    HttpContext = httpContext
                };
            });
            return services;
        }
    }
}
