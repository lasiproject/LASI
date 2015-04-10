using System;
using System.Linq;
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
using System.IO;
using AspSixApp.Models;
using AspSixApp.CustomIdentity;
using AspSixApp.CustomIdentity.MongoDB;
using AspSixApp.CustomIdentity.MongoDB.Extensions;
using LASI.Utilities;

namespace AspSixApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
               .AddJsonFile("config.json")
               .AddEnvironmentVariables();
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance<ILookupNormalizer>(new UpperInvariantLookupNormalizer());
            services.AddMvc().Configure<MvcOptions>(options =>
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    Error = (s, e) => { throw e.ErrorContext.Error; },
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                options.InputFormatters
                    .Select(formatter => formatter.Instance)
                    .OfType<JsonInputFormatter>()
                    .First().SerializerSettings = serializerSettings;
                options.OutputFormatters
                    .Select(formatter => formatter.Instance)
                    .OfType<JsonOutputFormatter>()
                    .First().SerializerSettings = serializerSettings;

                options.Filters.Add(new CustomAsyncExceptionFilter());
            });
            services
                .AddMongoDb(provider => new MongoDBConfiguration(Configuration.GetSubKey("Data"), AppDomain.CurrentDomain.BaseDirectory));
            services
                .AddIdentity<ApplicationUser, UserRole>()
                .AddRoleStore<CustomUserStore<UserRole>>()
                .AddUserStore<CustomUserStore<UserRole>>()
                .AddUserManager<UserManager<ApplicationUser>>();
            services.ConfigureIdentity(options =>
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
                options.Password = new PasswordOptions
                {
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequiredLength = 8
                };
            });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            ConfigureLASIComponents(fileName: Path.Combine(Directory.GetParent(env.WebRoot).FullName, "config.json"), subkey: "Data");

            // Configure the HTTP request pipeline. Add the loggers.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (env.EnvironmentName.EqualsIgnoreCase("Development"))
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }
            app.UseRuntimeInfoPage()
            // Add static files to the request pipeline.
                .UseStaticFiles()
            // Add cookie-based authentication to the request pipeline.
                .UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "ChildApi",
                    template: "api/{parentController}/{parentId?}/{controller}/{id?}"
                );
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "api/{controller}/{id?}"
                );
            });
        }
        /// <summary>
        /// Bootstrap LASI by loading the necessary configuration information from the specified file.
        /// </summary>
        /// <param name="fileName">The path to a JSON file containing the desired settings.</param>
        /// <remarks>
        /// In the desktop application and previous versions of the web application, the
        /// configuration settings were stored in App.config and Web.config respectively, and were
        /// thus automatically loaded into the System.ConfigurationManager.AppSettings property.
        /// This is not possible with the current build of AspNet 5, so this is implemented to fill
        /// the gap. A better solution, one which abstracts the configuration from the assemblies
        /// entirely should be designed and implemented.
        /// </remarks>
        private void ConfigureLASIComponents(string fileName, string subkey)
        {
            LASI.Interop.ResourceManagement.ResourceUsageManager.SetPerformanceLevel(LASI.Interop.ResourceManagement.PerformanceProfile.High);
            LASI.Interop.Configuration.Initialize(fileName, LASI.Interop.ConfigFormat.Json, subkey);
        }
        public IConfiguration Configuration { get; set; }
    }
}