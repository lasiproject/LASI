using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LASI.WebR.Startup))]
namespace LASI.WebR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
