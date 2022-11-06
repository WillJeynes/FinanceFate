using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinanceFate
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Securitry
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            //Main
            routes.MapRoute(
                name: "General Route",
                url: "{controller}/",
                defaults: new { controller = "Home", action = "Index", Root = "Home", Page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Genral Page Route",
                url: "{controller}/{Root}/{Page}/",
                defaults: new { controller = "None", action = "Index", Root = "Home", Page = UrlParameter.Optional }
            );

        }
    }
}
