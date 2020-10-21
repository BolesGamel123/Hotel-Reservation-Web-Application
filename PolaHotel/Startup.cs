using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PolaHotel.Startup))]
namespace PolaHotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
