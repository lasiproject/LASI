using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using AspSixApp.Models;
using AspSixApp.Models.ApplicationUsers;
using AspSixApp.Models.ApplicationUserRoles;
using System.Linq;
using MongoDB.AspNet.Identity;

namespace AspSixApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env) {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile("resources.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services) {

            // Add EF services to the services container.
            services.AddEntityFramework(Configuration)
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>();

            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, IdentityRole>(Configuration)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            #region   
            //Configuration.GetSubKeys()
            //    .ToList()
            //    .ForEach(setting => System.Configuration.ConfigurationManager.AppSettings.Add(
            //        setting.Key,
            //        setting.Value.ToString()));
            //services.AddSingleton(serviceProvider => new MongoDB.AspNet.Identity.UserStore<MongoDB.AspNet.Identity.IdentityUser>(Configuration));


            //services.AddSingleton(provider =>
            //    new Microsoft.AspNet.Identity.UserManager<IdentityUser>(
            //       store: new UserStore<IdentityUser>(),
            //       userValidators: Enumerable.Empty<Microsoft.AspNet.Identity.IUserValidator<IdentityUser>>(),
            //       passwordValidators: Enumerable.Empty<Microsoft.AspNet.Identity.PasswordValidator<IdentityUser>>(),
            //       msgProviders: Enumerable.Empty<Microsoft.AspNet.Identity.IIdentityMessageProvider>(),
            //       userNameNormalizer: new UserNameNormalizer(),
            //       optionsAccessor: new OptionAccessor<Microsoft.AspNet.Identity.IdentityOptions>(),
            //       tokenProviders: Enumerable.Empty<TokenProvider<IdentityUser>>(),
            //       passwordHasher: new Microsoft.AspNet.Identity.PasswordHasher<IdentityUser>(
            //           new Microsoft.Framework.OptionsModel.OptionsManager<Microsoft.AspNet.Identity.PasswordHasherOptions>(Enumerable.Empty<Microsoft.Framework.OptionsModel.IConfigureOptions<Microsoft.AspNet.Identity.PasswordHasherOptions>>())))
            //);
            ////services.AddContextAccessor(Configuration).AddScoped<HttpContext>(provider=>new HttpRequest().HttpContext)
            //var context = new ContextAccessor<HttpContext>(); /*context.SetValue(AppContext, SetContextSource(, context => context);*/
            //services.AddSingleton(provider =>
            //    new Microsoft.AspNet.Identity.SignInManager<IdentityUser>(
            //        userManager: provider.GetService<Microsoft.AspNet.Identity.UserManager<IdentityUser>>(),
            //        contextAccessor: context,
            //        claimsFactory: new Microsoft.AspNet.Identity.ClaimsIdentityFactory<IdentityUser, IdentityUserRole>(
            //            provider.GetService<Microsoft.AspNet.Identity.UserManager<IdentityUser>>(),
            //            new Microsoft.AspNet.Identity.RoleManager<IdentityUserRole>(new RoleStore<IdentityUserRole>(),
            //            Enumerable.Empty<Microsoft.AspNet.Identity.RoleValidator<IdentityUserRole>>()),
            //            new Microsoft.Framework.OptionsModel.OptionsManager<Microsoft.AspNet.Identity.IdentityOptions>(Enumerable.Empty<Microsoft.Framework.OptionsModel.IConfigureOptions<Microsoft.AspNet.Identity.IdentityOptions>>())),
            //        optionsAccessor: new Microsoft.Framework.OptionsModel.OptionsManager<Microsoft.AspNet.Identity.IdentityOptions>(Enumerable.Empty<Microsoft.Framework.OptionsModel.IConfigureOptions<Microsoft.AspNet.Identity.IdentityOptions>>()))

            //);
            //services.AddIdentity<MongoDB.AspNet.Identity.IdentityUser, MongoDB.AspNet.Identity.IdentityUser>(Configuration)
            //    .AddUserStore<MongoDB.AspNet.Identity.UserStore<MongoDB.AspNet.Identity.IdentityUser>>();
            //.AddRoleStore<UserStore<MongoDB.AspNet.Identity.IdentityUserRole>>();
            // Add MVC services to the services container.
            #endregion


            services.AddMvc();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory) {
            // Configure the HTTP request pipeline.
            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase)) {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            } else {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();
            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
