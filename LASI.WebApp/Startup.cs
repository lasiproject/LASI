using System;
using System.IO;
using LASI.WebApp.Authentication;
using LASI.WebApp.Logging;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using LASI.WebApp.Persistence;
using LASI.WebApp.Persistence.MongoDB.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

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
                    .AddSingleton<System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler>()
                    .AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>()
                    .AddSingleton<IWorkItemsService, WorkItemsService>()
                    .AddMongoDB(Configuration)
                    .AddRouting(options =>
                    {
                        options.LowercaseUrls = true;
                    });

            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Formatting = isDevelopment ? Formatting.Indented : Formatting.None;
                        options.SerializerSettings.Error = (s, e) => { throw e.ErrorContext.Error; };
                        options.SerializerSettings.Converters.Add(new StringEnumConverter
                        {
                            AllowIntegerValues = false,
                            CamelCaseText = true
                        });
                    });

            services//.AddSingleton(provider => TokenAuthorizationOptions)
                    .AddAuthorization(options =>
                    {
                        options.AddPolicy("Bearer", policy =>
                        {
                            policy
                               .AddAuthenticationSchemes("Bearer")
                               .RequireAuthenticatedUser()
                               .Build();
                        });
                    })
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
            RsaKey = new Microsoft.IdentityModel.Tokens.RsaSecurityKey(RSAFactory.LoadRSAKey(Configuration["AppSettings:KeyFileName"]));

            TokenAuthorizationOptions = new TokenAuthorizationOptions
            {
                Audience = "LASI",
                Issuer = "LASI",
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(key: RsaKey, algorithm: Microsoft.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256Signature)
            };

            loggerFactory
                .AddConsole(LogLevel.Debug)
                .AddLASIOutput(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app/*.UseBrowserLink()*/
                   .UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseIISPlatformHandler(options =>
               {
                   options.AuthenticationDescriptions.Clear();
                   options.AutomaticAuthentication = true;
               })
               .UseCookieAuthentication(options =>
               {
                   options.AutomaticAuthenticate = true;
                   options.AuthenticationScheme = "Bearer";
                   options.LoginPath = null;
                   options.LogoutPath = null;
               })
               //.UseJwtBearerAuthentication(options =>
               //{
               //    options.Events = new JwtBearerEvents
               //    {
               //        OnAuthenticationFailed = async context =>
               //        {
               //            System.Diagnostics.Debug.WriteLine(context.Exception.Message);
               //            context.Response.StatusCode = 401;
               //            await context.Response.Body.WriteAsync(Encoding.Default.GetBytes("Unauthorized"), 0, 12);
               //        }
               //    };
               //    options.RequireHttpsMetadata = false;
               //    options.TokenValidationParameters.IssuerSigningKey = RsaKey;
               //    options.TokenValidationParameters.ValidAudience = TokenAuthorizationOptions.Audience;
               //    options.TokenValidationParameters.ValidIssuer = TokenAuthorizationOptions.Issuer;
               //    options.TokenValidationParameters.ValidateSignature = true;
               //    options.TokenValidationParameters.ValidateLifetime = true;
               //    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(0);
               //    options.TokenValidationParameters.NameClaimType = "unique_name";
               //    options.AutomaticAuthenticate = false;
               //    options.AutomaticChallenge = false;

               //    options.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration
               //    {
               //        HttpLogoutSupported = true
               //    };
               //    options.TokenValidationParameters.SaveSigninToken = true;
               //})
               .UseCors(policy =>
               {
                   policy.AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowAnyOrigin()
                         .AllowCredentials()
                         .WithExposedHeaders("Access-Control-Allow-Origin");
               })
               .UseStaticFiles(new Microsoft.AspNet.StaticFiles.StaticFileOptions
               {
                   ServeUnknownFileTypes = true,
               })
               .UseIdentity()
               .UseMvc(routes =>
               {
                   routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                         .MapRoute(name: "ChildApi", template: "api/{parentController}/{parentId?}/{controller}/{id?}")
                         .MapRoute(name: "DefaultApi", template: "api/{controller}/{id?}");
               });
        }

        private void ConfigureLASIComponents(string fileName, string subkey)
        {
            Interop.ResourceUsageManager.SetPerformanceLevel(Interop.PerformanceProfile.High);
            Interop.Configuration.Initialize(fileName, Interop.ConfigFormat.Json, subkey);
        }


        public static void Main(string[] args)
        {
            //new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory()).UseDefaultHostConfiguration(args);
            WebApplication.Run<Startup>(args);
        }

        Microsoft.IdentityModel.Tokens.RsaSecurityKey RsaKey { get; set; }
        private readonly bool isDevelopment;
        private TokenAuthorizationOptions TokenAuthorizationOptions { get; set; }
    }
}