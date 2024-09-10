using SDSDemo.Interfaces;
using SDSDemo.Repositories;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace SDSDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SDSDemoContext"].ConnectionString;
            var container = new UnityContainer();
            container.RegisterType<IRecyclableTypeRepository, RecyclableTypeRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<IRecyclableItemRepository, RecyclableItemRepository>(new InjectionConstructor(connectionString));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
