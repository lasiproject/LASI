using System;
using System.IO;
using System.Linq;
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
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            environmentIsDevelopment = env.IsEnvironment("Development");
            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            ConfigureLASIComponents(fileName: Path.Combine(Directory.GetParent(env.WebRootPath).FullName, "config.json"), subkey: "Resources");
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetConfigurationSection("AppSettings"))
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
                    .ConfigureIdentity(options =>
                    {
                        options.Lockout = new LockoutOptions
                        {
                            AllowedForNewUsers = true,
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
                            RequiredLength = 8,
                            RequireDigit = true,
                            RequireLowercase = true,
                            RequireUppercase = true,
                            RequireNonLetterOrDigit = true
                        };
                    })
                    .AddMvc()
                    .ConfigureMvc(options =>
                    {
                        options.InputFormatters.OfType<JsonInputFormatter>().First().SerializerSettings = MvcJsonSerializerSettings;
                        options.OutputFormatters.OfType<JsonOutputFormatter>().First().SerializerSettings = MvcJsonSerializerSettings;
                    });
            services.AddIdentity<ApplicationUser, UserRole>()
                    .AddUserValidator<UserValidator<ApplicationUser>>()
                    .AddRoleManager<RoleManager<UserRole>>()
                    .AddRoleStore<CustomUserStore<UserRole>>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddUserStore<CustomUserStore<UserRole>>()
                    .AddDefaultTokenProviders();
            // Configure the options for the authentication middleware.
            // You can add options for Google, Twitter and other middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
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
                app.UseErrorPage(new ErrorPageOptions // Added to react to the removal of ErrorPageOptions.ShowAll from the next version.
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
            //app.UseFacebookAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                      .MapRoute(name: "ChildApi", template: "api/{parentController}/{parentId?}/{controller}/{id?}")
                      .MapRoute(name: "DefaultApi", template: "api/{controller}/{id?}");
            });
        }

        private void ConfigureLASIComponents(string fileName, string subkey)
        {
            LASI.Interop.ResourceUsageManager.SetPerformanceLevel(LASI.Interop.PerformanceProfile.High);
            LASI.Interop.Configuration.Initialize(fileName, LASI.Interop.ConfigFormat.Json, subkey);
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