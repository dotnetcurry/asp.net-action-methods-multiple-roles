using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_CustomRoles.Startup))]
namespace MVC_CustomRoles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
