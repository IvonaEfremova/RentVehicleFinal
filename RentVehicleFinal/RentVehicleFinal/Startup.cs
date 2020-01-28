using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentVehicleFinal.Startup))]
namespace RentVehicleFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
