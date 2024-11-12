using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(waats.Startup))]
namespace waats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
