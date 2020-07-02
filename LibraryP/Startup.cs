using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryP.Startup))]
namespace LibraryP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
