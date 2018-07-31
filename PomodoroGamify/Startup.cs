using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PomodoroGamify.Startup))]
namespace PomodoroGamify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
