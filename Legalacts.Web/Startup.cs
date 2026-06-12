using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Legalacts.Web.Startup))]
namespace Legalacts.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
