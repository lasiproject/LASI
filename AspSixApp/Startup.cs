using System;
using System.Linq;
using AspSixApp.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AspSixApp
{
    using BuilderExtensions;
    using LASI.Utilities;
    using Path = System.IO.Path;

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
            //services.AddTypeActivator<
            // Add MVC services to the services container.
            services.AddMvc()
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

            // Add EF services to the services container.
            services
                .AddEntityFramework(Configuration)
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>();

            // Add Identity services to the services container.
            services
                .AddIdentity<ApplicationUser, IdentityRole>(Configuration)
                //.AddPasswordValidator<PasswordValidator<ApplicationUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            app.UseLASIComponents(configFile: Path.Combine(AppContext.BaseDirectory, "resources.json"));

            // Configure the HTTP request pipeline. Add the console logger.
            loggerfactory
                .AddConsole()
                .AddLASIOutput();

            // Add the following to the request pipeline only in development environment.
            if (env.EnvironmentName.EqualsIgnoreCase("Development"))
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

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

        public IConfiguration Configuration { get; set; }
    }
}