using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
         

            routes.MapRoute(
                name: "GetSanPham",
                url: "SanPham/Get/{id}",
                defaults: new { controller = "SanPham", action = "Get"}
            );

            routes.MapRoute(
                name: "GetBrand",
                url: "SanPhams/GetBrand/{id}/{page}",
                defaults: new { controller = "SanPham", action = "GetBrand" }
            );


            routes.MapRoute(
               name: "TimKiemTuKhoa",
               url: "{controller}/{action}/{tukhoa}",
               defaults: new { controller = "SanPhams", action = "TimKiem"}
           );
        }
    }
}