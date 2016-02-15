using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UAHFitVault.App_Start;
using UAHFitVault.Database;

namespace UAHFitVault
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // seed the database
            //System.Data.Entity.Database.SetInitializer(new AppDbSeedData());
            //System.Data.Entity.Database.SetInitializer(new FitVaultDbSeedData());

            // Autofac and Automapper configurations
            Bootstrapper.Run();
        }
    }
}
