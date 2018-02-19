using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace LASI.WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(ConfigureCorsPolicy);
                })
                .AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            env.WebRootPath = "/";
            //env.WebRootFileProvider= new
            var root = Path.Combine(Directory.GetCurrentDirectory());

            app.UseCors(ConfigureCorsPolicy)
               .UseStaticFiles(new StaticFileOptions
               {
                   FileProvider = new PhysicalFileProvider(root),
                   ServeUnknownFileTypes = true,
                   DefaultContentType = "text/plain",
                   RequestPath = new PathString("")
               })
               .UseDefaultFiles();
        }

        static void ConfigureCorsPolicy(CorsPolicyBuilder policy) =>
            policy.SetIsOriginAllowedToAllowWildcardSubdomains()
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
    }
}
