using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "Movie Index",
            //    url: "movies/{pageIndex}/{name}",
            //    defaults: new { controller = "Movies", action = "Index", pageIndex = UrlParameter.Optional, name = UrlParameter.Optional },
            //    new {pageIndex =@"\d{3}"}
            //);

            //routes.MapRoute(
            //    name: "Movie",
            //    url: "Movies/{movieId}",
            //    defaults: new { controller = "Movies", action = "Edit", movieId = UrlParameter.Optional },
            //    constraints: new {movieId = @"\d{5}"}
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
