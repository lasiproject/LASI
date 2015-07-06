using System;
using System.IO;
using LASI.WebApp.Logging;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using LASI.WebApp.Persistence;
using LASI.WebApp.Persistence.MongoDB.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
            environmentIsDevelopment = env.IsEnvironment("Development");
            if (environmentIsDevelopment)
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                configuration.AddUserSecrets();
            }
            configuration.AddEnvironmentVariables();
            Configuration = configuration;
            ConfigureLASIComponents(fileName: Path.Combine(Directory.GetParent(env.WebRootPath).FullName, "config.json"), subkey: "Resources");
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.Configure<AppSettings>(Configuration.GetSubKey("AppSettings"))
                    .AddSingleton<ILookupNormalizer>(provider => new UpperInvariantLookupNormalizer())
                    .AddSingleton<IWorkItemsService>(provider => new WorkItemsService())
                    .AddMongoDB(options =>
                    {
                        options.CreateProcess = true;
                        options.ApplicationBasePath = AppDomain.CurrentDomain.BaseDirectory;
                        options.UserCollectionName = "users";
                        options.UserDocumentCollectionName = "documents";
                        options.OrganizationCollectionName = "organizations";
                        options.UserRoleCollectionName = "roles";
                        options.ApplicationDatabaseName = "accounts";
                        options.MongodExePath = Configuration["MongoDB:MongodExePath"];
                        options.DataDbPath = Configuration["MongoDB:MongoDataDbPath"];
                        options.InstanceUrl = Configuration["MongoDB:MongoDbInstanceUrl"];
                    })
                    .AddIdentity<ApplicationUser, UserRole>(options =>
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
                        options.Password = environmentIsDevelopment ? new PasswordOptions { } : new PasswordOptions
                        {
                            RequireLowercase = true,
                            RequireUppercase = true,
                            RequireNonLetterOrDigit = true,
                            RequireDigit = true,
                            RequiredLength = 8
                        };
                    })
                    .AddDefaultTokenProviders()
                    .AddUserValidator<UserValidator<ApplicationUser>>()
                    .AddRoleManager<RoleManager<UserRole>>()
                    .AddRoleStore<CustomUserStore<UserRole>>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddUserStore<CustomUserStore<UserRole>>();

            services.AddMvc()
                    .ConfigureMvc(options =>
                    {
                        options.InputFormatters.InstanceOf<JsonInputFormatter>().SerializerSettings = MvcJsonSerializerSettings;
                        options.OutputFormatters.InstanceOf<JsonOutputFormatter>().SerializerSettings = MvcJsonSerializerSettings;
                    });

            // Configure the options for the authentication middleware.
            // You can add options for Google, Twitter and other middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            services.ConfigureFacebookAuthentication(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug)
                         .AddLASIOutput(LogLevel.Debug);

            app.Properties["host.AppMode"] = "development";

            app.UseStatusCodePages()
               .UseFileServer()/*.UseDirectoryBrowser()*/
               .UseBrowserLink()
               .UseIdentity()
               .UseDefaultFiles()
               .UseRuntimeInfoPage();
            // Add the following to the request pipeline only in development environment.
            if (environmentIsDevelopment)
            {
                app.UseErrorPage(new ErrorPageOptions() // Added to react to the removal of ErrorPageOptions.ShowAll from the next version.
                {
                    ShowCookies = true,
                    ShowEnvironment = true,
                    ShowExceptionDetails = true,
                    ShowHeaders = true,
                    ShowQuery = true,
                    ShowSourceCode = true
                });
            }
            else
            {
                app.UseErrorHandler("/Home/Error");
            }

            // Add authentication middleware to the request pipeline. You can configure options such as Id and Secret in the ConfigureServices method.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            // app.UseFacebookAuthentication(); app.UseGoogleAuthentication(); app.UseMicrosoftAccountAuthentication(); app.UseTwitterAuthentication();

            app.UseMvc(routes => routes
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}")
                .MapRoute(
                    name: "ChildApi",
                    template: "api/{parentController}/{parentId?}/{controller}/{id?}")
                .MapRoute(
                    name: "DefaultApi",
                    template: "api/{controller}/{id?}")
            );

        }

        private void ConfigureLASIComponents(string fileName, string subkey)
        {
            Interop.ResourceUsageManager.SetPerformanceLevel(Interop.PerformanceProfile.High);
            Interop.Configuration.Initialize(fileName, Interop.ConfigFormat.Json, subkey);
        }

        private static readonly JsonSerializerSettings MvcJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
#if DEBUG
            Formatting = Formatting.Indented,
            Error = (s, e) => { throw e.ErrorContext.Error; },
#endif
            Converters = new[] { new StringEnumConverter { AllowIntegerValues = false, CamelCaseText = true } }
        };

        private bool environmentIsDevelopment;

    }
}