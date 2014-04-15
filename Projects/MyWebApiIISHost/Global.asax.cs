using Microsoft.Owin;
using Owin;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyWebApiIISHost
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapOwinPath("/rss", app =>
            {
                app.Run((IOwinContext context) =>
                {
                    context.Response.ContentType = "application/rss+xml";

                    var response = new System.Text.StringBuilder();
                    response.Append(
                        "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>")
                        .Append("<rss version=\"2.0\">")
                        .Append("<channel>")
                        .Append("<title>My RSS Feed</title>")
                        .Append("<item>")
                        .Append("<description>Hail K &amp; R")
                        .Append("</description>")
                        .Append("<link>http://hello.world</link>")
                        .Append("</item></channel></rss>");

                    return context.Response.WriteAsync(response.ToString());
                });
            });

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}