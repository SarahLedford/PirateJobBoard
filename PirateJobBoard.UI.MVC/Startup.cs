using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PirateJobBoard.UI.MVC.Startup))]
namespace PirateJobBoard.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
