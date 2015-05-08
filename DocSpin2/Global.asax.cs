using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DocSpin2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			using (var context = new DocSpin2.Models.DataModelContainer())
			{
				context.Database.Initialize(false);
			}

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
