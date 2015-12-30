using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using LASI.WebApp.Authentication;
using LASI.WebApp.Authorization;
using LASI.WebApp.Filters;
using LASI.WebApp.Logging;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using LASI.WebApp.Persistence;
using LASI.WebApp.Persistence.MongoDB.Extensions;
using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
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
            tokenAuthorizationOptions = new TokenAuthorizationOptions
            {
                Audience = "LASI",
                Issuer = "LASI",
                SigningCredentials = new SigningCredentials(rsaKey = new RsaSecurityKey(new RSACryptoServiceProvider(2048).ExportParameters(true)), SecurityAlgorithms.RsaSha256Signature)
            };
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
                    .AddMongoDB(Configuration)
                    .AddMvc(options =>
                    {
                        //options.Filters.Add(new HttpResponseExceptionFilter());
                    })
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.Error = (s, e) => { throw e.ErrorContext.Error; };
                        options.SerializerSettings.Converters.Add(new StringEnumConverter
                        {
                            AllowIntegerValues = false,
                            CamelCaseText = true
                        });
                        options.SerializerSettings.Formatting = isDevelopment ? Formatting.Indented : Formatting.None;
                    });

            services.AddInstance(tokenAuthorizationOptions)
                    .AddAuthorization(options =>
                    {
                        options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                            .RequireAuthenticatedUser().Build()
                        );
                    })
                    //.AddAuthentication()
                    .AddIdentity<ApplicationUser, UserRole>(options =>
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
                        options.Password = new PasswordOptions
                        {
                            RequiredLength = 8,
                            RequireDigit = true,
                            RequireLowercase = true,
                            RequireUppercase = true,
                            RequireNonLetterOrDigit = true
                        };
                    })
                    .AddRoleStore<CustomUserStore<UserRole>>()
                    .AddUserStore<CustomUserStore<UserRole>>();
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
            app.UseIISPlatformHandler(options =>
                {
                    options.AuthenticationDescriptions.Clear();
                    options.AutomaticAuthentication = false;
                })
               .UseJwtBearerAuthentication(options =>
               {
                   options.TokenValidationParameters.IssuerSigningKey = rsaKey;
                   options.TokenValidationParameters.ValidAudience = tokenAuthorizationOptions.Audience;
                   options.TokenValidationParameters.ValidIssuer = tokenAuthorizationOptions.Issuer;
                   options.TokenValidationParameters.ValidateSignature = true;
                   options.TokenValidationParameters.ValidateLifetime = true;
                   options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(0);
               })
               .UseIdentity()
               .UseStaticFiles()
               .UseCors(policy =>
               {
                   policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithExposedHeaders("Access-Control-Allow-Origin");
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
        private readonly SecurityKey rsaKey = null;
        private readonly TokenAuthorizationOptions tokenAuthorizationOptions;
    }
}