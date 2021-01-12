using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lecture6Vidly.Startup))]
namespace Lecture6Vidly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
