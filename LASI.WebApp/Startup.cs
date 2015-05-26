using System;
using System.IO;
using System.Linq;
using LASI.Utilities;
using LASI.WebApp.CustomIdentity;
using LASI.WebApp.CustomIdentity.MongoDB.Extensions;
using LASI.WebApp.Helpers;
using LASI.WebApp.Logging;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.Authentication.Facebook;
using Microsoft.AspNet.Authentication.MicrosoftAccount;
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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using LASI.WebApp.CustomIdentity.MongoDB;

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

            if (env.IsEnvironment("Development"))
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                configuration.AddUserSecrets();
            }
            configuration.AddEnvironmentVariables();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //loggerFactory.AddConsole().AddLASIOutput();
            //var consoleLogger = loggerFactory.CreateLogger<ConsoleLogger>();
            //var lasiLogger = loggerFactory.AddLASIOutput().CreateLogger<ConsoleLogger>();
            //var log = ((Action<string>)consoleLogger.LogCritical).AndThen(lasiLogger.LogCritical);
            services.Configure<AppSettings>(Configuration.GetSubKey("AppSettings"));

            services
                .AddTransient<ILookupNormalizer>(provider => new UpperInvariantLookupNormalizer())
                .AddSingleton<IWorkItemsService>(provider => new DummyUserWorkItemService(
                        itemCount: 5,
                        maxUpdate: 10,
                        updateInterval: TimeSpan.FromSeconds(5)
                    )
                );

            services
                .AddMongoDB(new MongoDBConfiguration(Configuration.GetSubKey("Data"), AppDomain.CurrentDomain.BaseDirectory));

            //.AddMongoDB(options =>
            //{
            //    options.UserCollectionName = "users";
            //    options.UserDocumentCollectionName = "documents";
            //    options.OrganizationCollectionName = "organizations";
            //    options.UserRoleCollectionName = "roles";
            //    options.ApplicationBasePath = AppDomain.CurrentDomain.BaseDirectory;
            //    options.ApplicationDatabaseName = "accounts";
            //    options.MongodExePath = Configuration["MongodExecutableLocation"];
            //    options.CreateProcess = true;
            //    options.DataDbPath = Configuration["MongoDataDbPath"];
            //    options.InstanceUrl = Configuration["MongoDbInstanceUrl"];
            //});

            services
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
                    options.Password = new PasswordOptions
                    {
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequireNonLetterOrDigit = true,
                        RequireDigit = true,
                        RequiredLength = 8
                    };
                })
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<UserRole>>()
                .AddRoleStore<CustomUserStore<UserRole>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddUserStore<CustomUserStore<UserRole>>();


            services
                .AddMvc()
                .ConfigureMvc(options =>
                {
                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
#if OPTIMIZE // For release builds we prefer the more performant, small payload.
                        Formatting = Formatting.Indented,
#endif
                        Error = (s, e) =>
                        {
                            var x = e.ErrorContext.Error;
                            //log($"{x.Message}\n{x.StackTrace}");
#if DEBUG // Fail in debug builds
                            throw x;
#endif
                        },
                        Converters = new[] { new StringEnumConverter { AllowIntegerValues = false, CamelCaseText = true } }
                    };
                    options.InputFormatters.FirstDescribing<JsonInputFormatter>().SerializerSettings = serializerSettings;
                    options.OutputFormatters.FirstDescribing<JsonOutputFormatter>().SerializerSettings = serializerSettings;
                });

            // Configure the options for the authentication middleware.
            // You can add options for Google, Twitter and other middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            services
                .ConfigureFacebookAuthentication(options =>
                {
                    options.AppId = Configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .ConfigureMicrosoftAccountAuthentication(options =>
                {
                    options.ClientId = Configuration["Authentication:MicrosoftAccount:ClientId"];
                    options.ClientSecret = Configuration["Authentication:MicrosoftAccount:ClientSecret"];
                });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            ConfigureLASIComponents(fileName: Path.Combine(Directory.GetParent(env.WebRootPath).FullName, "config.json"), subkey: "Data");
            // Configure the HTTP request pipeline.

            // Add the console logger.
            //loggerfactory
            //    .AddConsole(LogLevel.Debug)
            //    .AddLASIOutput(LogLevel.Debug);

            // Add static files to the request pipeline.
            app.UseStaticFiles()
                .UseStaticFiles("/app/widgets");

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add the following to the request pipeline only in development environment.
            if (env.IsEnvironment("Development"))
            {
                app.UseBrowserLink()
                   .UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and sends the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add authentication middleware to the request pipeline. You can configure options such as Id and Secret in the ConfigureServices method.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            // app.UseFacebookAuthentication();
            // app.UseGoogleAuthentication();
            // app.UseMicrosoftAccountAuthentication();
            // app.UseTwitterAuthentication();

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

        private void ConfigureLASIComponents(string fileName, string subkey)
        {
            Interop.ResourceManagement.ResourceUsageManager.SetPerformanceLevel(Interop.ResourceManagement.PerformanceProfile.High);
            Interop.Configuration.Initialize(fileName, Interop.ConfigFormat.Json, subkey);
        }
    }
}
