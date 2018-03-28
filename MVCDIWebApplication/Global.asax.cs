using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using MVCDIWebApplication.ErrorLogger;
using Unity;
using Unity.AspNet.Mvc;

namespace MVCDIWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IUnityContainer container = new UnityContainer();
            ExceptionLogging _ErrorLog = new ExceptionLogging();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // allocate filter and add it to global configuration
            var exceptionLogger = new ExceptionLoggerFilter(_ErrorLog);
            GlobalConfiguration.Configuration.Filters.Add(exceptionLogger);

            UnityConfig.RegisterTypes(container);
        }
    }
}
