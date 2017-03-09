using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMY.PlatForm.WebUI.Startup))]
namespace MMY.PlatForm.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
