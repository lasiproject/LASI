using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using AspSixApp.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AspSixApp
{
    using Path = System.IO.Path;
    using LASIConfig = LASI.Utilities.IConfig;
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            var path = Path.Combine(AppContext.BaseDirectory, "resources.json");
            ConfigureLASIComponents(path);
        }

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add MVC services to the services container.
            services
                .AddMvc()
                .AddWebApiConventions()
                .Configure<MvcOptions>(options =>
                {
                    var serializerSettings = new JsonSerializerSettings
                    {
                        Error = (sender, e) => { throw e.ErrorContext.Error; },
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.Indented
                    };
                    options.OutputFormatters.Select(of => of.Instance).OfType<JsonOutputFormatter>().First().SerializerSettings = serializerSettings;

                });

            // Add EF services to the services container.
            services
                .AddEntityFramework(Configuration)
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>();

            // Add Identity services to the services container.
            services
                .AddIdentity<ApplicationUser, IdentityRole>(Configuration)
                .AddEntityFrameworkStores<ApplicationDbContext>();

        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Configure the HTTP request pipeline.
            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
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

        private void ConfigureLASIComponents(string configFilePath)
        {
            var configSource = new LASI.Utilities.JsonConfig(configFilePath);
            TaggerInterop.SharpNLPTagger.InjectedConfiguration = configSource;
            LASI.Core.Heuristics.Lexicon.InjectedConfiguration = configSource;
        }
    }
}
