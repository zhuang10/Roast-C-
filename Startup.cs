using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoastMeApplication.Startup))]
namespace RoastMeApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
