using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPS.Web.Startup))]
namespace TPS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
