using LASI.WebApp.Models;
using LASI.WebApp.Persistence;
using LASI.WebApp.Tests.ServiceCollectionExtensions;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            services.AddScoped(provider => new RouteData { })
                    .AddScoped<HttpContext, DefaultHttpContext>()
                    .AddScoped<IHttpContextAccessor, HttpContextAccessor>()
                    .AddInMemoryStores(user)
                    .AddLogging()
                    .AddSingleton(provider => new LoggerFactory().AddConsole(LogLevel.Critical))
                    //.AddSingleton<ILogger<UserManager<ApplicationUser>>>(provider => new Logger<UserManager<ApplicationUser>>(provider.GetService<ILoggerFactory>()))
                    .AddSingleton<UserClaimsPrincipalFactory<ApplicationUser, UserRole>>()
                    .AddSingleton<IAuthorizationService, DefaultAuthorizationService>()
                    .AddSingleton<SignInManager<ApplicationUser>>()
                    .AddScoped<UserClaimsPrincipalFactory<ApplicationUser, UserRole>>()
                    .AddScoped<ActionDescriptor>()
                    .AddSingleton(provider =>
                    {
                        var userClaimsPrincipalFactory = provider.GetRequiredService<UserClaimsPrincipalFactory<ApplicationUser, UserRole>>();
                        var httpContext = provider.GetRequiredService<HttpContext>();
                        var userClaimsPrincipal = userClaimsPrincipalFactory.CreateAsync(user);

                        httpContext.User = userClaimsPrincipal.Result;
                        return new ActionContext
                        {
                            HttpContext = httpContext,
                            RouteData = new RouteData(),
                            ActionDescriptor = provider.GetRequiredService<ActionDescriptor>()
                        };
                    });
            return services;
        }
    }
}