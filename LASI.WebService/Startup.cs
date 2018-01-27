using System.Collections.Generic;
using LASI.Utilities.Configuration;
using LASI.WebService.Config;
using LASI.WebService.Data;
using LASI.WebService.Extensions;
using LASI.WebService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LASI.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IValuesService, ValuesService>()
                .AddSingleton(Configuration)
                .AddDbContext<ApplicationDbContext>((options) =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                })
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    JsonConfiguration.AddDefaultSerializerSettings(options.SerializerSettings);
                })
                .AddMvcOptions(options => {})
                .AddRazorOptions(options =>
                {
                    options.CompilationCallback = e => System.Console.WriteLine(e.Compilation);
                })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            services.AddSingleton<IEnumerable<(string key, int value)>>(provider => new []
            {
                (key: "A", value : 1)
            });

            // Register no-op EmailSender used by account confirmation and password reset during development For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSignalR(options =>
            {
                JsonConfiguration.AddDefaultSerializerSettings(options.JsonSerializerSettings);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var (isDevelopment, _, _) = env;
            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage()
                    .UseBrowserLink()
                    .UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles()
                .UseAuthentication()
                .UseSignalR(options =>
                {
                    options.MapHub<JwtBroadcaster>("/broadcast");
                })
                .UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                    // .MapRoute(
                    // name: "api",
                    // template: "api/{controller}/{id?}");
                });

            Interop.Configuration.Initialize(new JsonConfig("./appsettings.json"));
        }
    }

}