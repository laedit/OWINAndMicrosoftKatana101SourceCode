using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebApiIISHost.Startup))]
namespace MyWebApiIISHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
