using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kissi.Startup))]
namespace Kissi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
