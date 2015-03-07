using System;
using System.Linq;
using AspSixApp.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AspSixApp.Logging;
using LASI.Utilities;
using System.IO;
using AspSixApp.CustomIdentity;
using AspSixApp.CustomIdentity.MongoDb;

namespace AspSixApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
               //.AddJsonFile("config.json")
               .AddJsonFile("resources.json")
               .AddEnvironmentVariables();
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {    // Add MVC services to the services container.
            services
                .AddMvc()
                .AddWebApiConventions()
                .Configure<MvcOptions>(options =>
                {
                    options.OutputFormatters
                        .Select(formatter => formatter.Instance)
                        .OfType<JsonOutputFormatter>().First()
                        .SerializerSettings = new JsonSerializerSettings
                        {
                            Error = (sender, e) => { throw e.ErrorContext.Error; },
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore,
                            Formatting = Formatting.Indented
                        };
                });

            services
                .AddSingleton(provider => new MongoConfiguration(Configuration, AppDomain.CurrentDomain))
                .AddSingleton<IInputDocumentStore<UserDocument>>(provider => new MongoDbInputDocumentStore(provider.GetService<MongoConfiguration>()))
                .AddSingleton<RoleProvider<UserRole>>(provider => new InMemoryRoleProvider())
                .AddSingleton<ILookupNormalizer>(provider => new UpperInvariantLookupNormalizer())
                .AddSingleton<UserProvider<ApplicationUser>>(provider => new MongoDbUserProvider(Configuration, AppDomain.CurrentDomain));

            services
                .AddIdentity<ApplicationUser, UserRole>(Configuration, options =>
                {
                    options.Lockout = new LockoutOptions
                    {
                        EnabledByDefault = true,
                        DefaultLockoutTimeSpan = TimeSpan.FromDays(1),
                        MaxFailedAccessAttempts = 10
                    };
                    options.User = new UserOptions
                    {
                        RequireUniqueEmail = true
                    };
                    options.SignIn = new SignInOptions
                    {
                        RequireConfirmedEmail = false,
                        RequireConfirmedPhoneNumber = false
                    };
                })
                .AddUserStore<CustomUserStore<UserRole>>()
                .AddRoleStore<CustomUserStore<UserRole>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleStore<CustomUserStore<UserRole>>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            ConfigureLASIComponents(configFilePath: Path.Combine(Directory.GetParent(env.WebRoot).FullName, "resources.json"));

            // Configure the HTTP request pipeline. Add the console logger.
            loggerfactory
                .AddConsole()
                .AddLASIOutput();

            app.UseBrowserLink();

            // Add the following to the request pipeline only in development environment.
            if (env.EnvironmentName.EqualsIgnoreCase("Development"))
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }
            app.UseRuntimeInfoPage();

            // Add static files to the request pipeline.
            app.UseStaticFiles();
            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapWebApiRoute(
                    name: "DefaultApi",
                    template: "api/{controller}/{id?}"
                );
            });
        }
        /// <summary>
        /// Bootstrap LASI by loading the necessary configuration information from the specified file.
        /// </summary>
        /// <param name="configFilePath">The location of a JSON file containing the desired settings.</param>
        /// <remarks>
        /// In the desktop application and previous versions of the web application, the
        /// configuration settings were stored in App.config and Web.config respectively, and were
        /// thus automatically loaded into the System.ConfigurationManager.AppSettings property.
        /// This is not possible with the current build of AspNet 5, so this is implemented to fill
        /// the gap. A better solution, one which abstracts the configuration from the assemblies
        /// entirely should be designed and implemented.
        /// </remarks>
        private void ConfigureLASIComponents(string configFilePath)
        {
            LASI.Interop.ResourceManagement.ResourceUsageManager.SetPerformanceLevel(LASI.Interop.ResourceManagement.PerformanceLevel.High);
            LASI.Interop.Configuration.Initialize(configFilePath, LASI.Interop.ConfigFormat.Json);
        }
        public IConfiguration Configuration { get; set; }
    }
}