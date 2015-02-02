using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Builder.Extensions;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Startup;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace AspSixApp.Tests
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory) {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

            app.Run(async context => {
                if (context.Request.Method == "POST") {
                    using (var reader = new System.IO.StreamReader(context.Request.Body)) {
                        var content = await reader.ReadToEndAsync();
                        if (content == "exit") {
                            await context.Response.SendAsync("shutting down");
                            Environment.Exit(0);
                        } else {
                            await context.Response.SendAsync($"you said {content}");
                        }
                    }
                }
                await context.Response.SendAsync("running this shit");
            });
        }
        public void ConfigureServices(IServiceCollection services) {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new DiagnosticsLoggerProvider());
            services.AddLogging();
        }
        //public static void Main(string[] args) {

        //}
    }
}
