using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMY.FrontSite.WebUI.Startup))]
namespace MMY.FrontSite.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
