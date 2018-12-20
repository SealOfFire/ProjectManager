using Microsoft.Owin;
using Owin;
using ProjectManager.Log;

[assembly: OwinStartup(typeof(ProjectManager.WebApplication.Startup))]
namespace ProjectManager.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Logger.Info("owin startup");
            this.Configuration(app);
        }
    }
}