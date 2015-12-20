using System.IO;
using System.Linq;
using System.Text;
using LASI.WebApp.Logging;
using LASI.WebApp.Models.User;
using LASI.WebApp.Persistence;
using LASI.WebApp.Persistence.MongoDB.Extensions;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            isDevelopment = env.IsDevelopment();
            if (isDevelopment)
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            ConfigureLASIComponents(fileName: Path.Combine(Directory.GetParent(env.WebRootPath).FullName, "appsettings.json"), subkey: "Resources");
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                    .AddSingleton<ILookupNormalizer>(provider => new UpperInvariantLookupNormalizer())
                    .AddSingleton<IWorkItemsService>(provider => new WorkItemsService())
                    .AddInstance(new Filters.HttpResponseExceptionFilter())
                    //.AddInMemoryDatabase()
                    .AddMongoDB(Configuration)
                    .AddMvc(options =>
                    {
                        options.Filters.AddService(typeof(Filters.HttpResponseExceptionFilter));
                    })
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.Error = (s, e) => { throw e.ErrorContext.Error; };
                        options.SerializerSettings.Converters = new[] { new StringEnumConverter { AllowIntegerValues = false, CamelCaseText = true } };
                        options.SerializerSettings.Formatting = isDevelopment ? Formatting.Indented : Formatting.None;
                    });

            services.AddAntiforgery()
                    .AddAuthorization()
                    .AddAuthentication();

            services.AddIdentity<Models.ApplicationUser, UserRole>(options =>
                    {
                        options.Lockout = new LockoutOptions
                        {
                            AllowedForNewUsers = true,
                            DefaultLockoutTimeSpan = System.TimeSpan.FromDays(1),
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
                            RequiredLength = 8,
                            RequireDigit = true,
                            RequireLowercase = true,
                            RequireUppercase = true,
                            RequireNonLetterOrDigit = true
                        };
                    })
                    .AddRoleValidator<RoleValidator<UserRole>>()
                    .AddUserValidator<UserValidator<Models.ApplicationUser>>()
                    .AddRoleStore<CustomUserStore<UserRole>>()
                    .AddUserStore<CustomUserStore<UserRole>>()
                    .AddDefaultTokenProviders();
            // Configure the options for the authentication middleware.
            // You can add options for Google, Twitter and other middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole(LogLevel.Debug)
                .AddLASIOutput(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink()
                   .UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles()
               .UseIdentity()
               .UseIISPlatformHandler(options =>
               {
                   options.AuthenticationDescriptions.Clear();
               })
               .UseCookieAuthentication(options =>
               {
                   //options.ReturnUrlParameter = "/#/login";
                   options.AutomaticChallenge = true;
                   options.AutomaticAuthenticate = true;
                   options.AccessDeniedPath = null;
               })
               .UseMvc(routes =>
               {
                   routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                         .MapRoute(name: "ChildApi", template: "api/{parentController}/{parentId?}/{controller}/{id?}")
                         .MapRoute(name: "DefaultApi", template: "api/{controller}/{id?}");
               });

        }
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        private void ConfigureLASIComponents(string fileName, string subkey)
        {
            Interop.ResourceUsageManager.SetPerformanceLevel(Interop.PerformanceProfile.High);
            Interop.Configuration.Initialize(fileName, Interop.ConfigFormat.Json, subkey);
        }
        private readonly bool isDevelopment;

    }
}