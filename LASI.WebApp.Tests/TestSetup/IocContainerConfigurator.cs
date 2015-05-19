using LASI.WebApp.CustomIdentity;
using LASI.WebApp.Models;
using LASI.WebApp.Tests.MvcHelpers;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;

namespace LASI.WebApp.Tests.TestSetup
{
    public static class IocContainerConfigurator
    {
        public static IServiceCollection CreateConfiguredServiceCollection(ApplicationUser applicationUser)
        {
            var services = new ServiceCollection();
            services.AddSingleton(provider => applicationUser)
                    .AddInMemoryStores(applicationUser)
                    .AddMvc()
                    .ConfigureMvc(MvcConfigurationHelper.ConfigureMvcJsonFormatters);

            services
                .AddIdentity<ApplicationUser, UserRole>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddUserStore<CustomUserStore<UserRole>>()
                .AddRoleManager<RoleManager<UserRole>>()
                .AddRoleStore<CustomUserStore<UserRole>>();
            return services
                .AddTransient(provider =>
                {
                    var user = provider.GetService<ApplicationUser>();
                    var httpContext = new Microsoft.AspNet.Http.Core.DefaultHttpContext();
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
                    var signInManager = new SignInManager<ApplicationUser>(
                        userManager,
                        contextAccessor,
                        userClaimsPrincipalFactory
                    );
                    httpContext.User = userClaimsPrincipal.Result;
                    signInManager.SignInAsync(user, true);
                    return new ActionContext
                    {
                        HttpContext = httpContext
                    };
                })
                .WithControllersAsServices(new[] { typeof(LASI.WebApp.Startup).Assembly });
        }
    }
}
