using System.Collections.Generic;
using LASI.Utilities.Configuration;
using LASI.WebService.Configuration;
using LASI.WebService.Data;
using LASI.WebService.Extensions;
using LASI.WebService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services
                .AddSingleton(Configuration)
                .AddDbContext<ApplicationDbContext>((options) =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                })
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    JsonConfiguration.AddDefaultSerializerSettings(options.SerializerSettings);
                })
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                })
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

            services
                .AddCors()
                .AddSockets()
                .AddSignalR()
                .AddJsonProtocol(options =>
                {
                    JsonConfiguration.AddDefaultSerializerSettings(options.PayloadSerializerSettings);
                });

            services.AddDbContext<DocumentsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DocumentsContext"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var isDevelopment = env.IsDevelopment();
            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage()
                    // .UseBrowserLink()
                    .UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles()
                .UseCors(policy =>
                {
                    policy.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                })
                .UseWebSockets()
                .UseAuthentication()
                .UseSignalR(options =>
                {
                    options.MapHub<JwtBroadcaster>("/broadcast", socket =>
                    {
                        socket.LongPolling.PollTimeout = 120. Seconds();
                        socket.WebSockets.CloseTimeout = 100. Seconds();
                    });

                    options.MapHub<UploadProgressBroadcaster>("/upload/{id}/progress");
                })
                .UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                    // .MapRoute(
                    // name: "api",
                    // template: "api/{controller}/{id?}");
                })
                .UseRewriter(new RewriteOptions().AddRedirectToHttps());

            Interop.Configuration.Initialize("./appsettings.json", Interop.ConfigurationFormat.Json, "Resources");
        }
    }

}