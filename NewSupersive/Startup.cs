using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewSupersive.Startup))]
namespace NewSupersive
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
