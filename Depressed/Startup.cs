using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Depressed.Startup))]
namespace Depressed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
