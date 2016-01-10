using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UAHFitVault.Startup))]
namespace UAHFitVault
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
