using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blogcatch.Startup))]
namespace Blogcatch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
