using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Extensions;

[assembly: OwinStartup(typeof(LASI.WebApp.Old.Startup))]

namespace LASI.WebApp.Old
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.Use(async (context, next) => {
                var authenticationResult = await context.Authentication.AuthenticateAsync("oauth");

            });
        }
    }
}
