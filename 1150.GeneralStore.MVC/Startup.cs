using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1150.GeneralStore.MVC.Startup))]
namespace _1150.GeneralStore.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
