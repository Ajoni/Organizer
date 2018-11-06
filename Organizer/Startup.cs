using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Organizer.Startup))]
namespace Organizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
