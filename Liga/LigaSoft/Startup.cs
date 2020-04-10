using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LigaSoft.Startup))]
namespace LigaSoft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
