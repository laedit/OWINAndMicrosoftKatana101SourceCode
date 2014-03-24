using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            app.UseWebApi(config);
            
            app.Run(async (IOwinContext context) =>
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes("<h1>Hello World</h1>");
                
                context.Response.ContentLength = bytes.Length;
                
                await context.Response.WriteAsync(bytes);
            });
        }
    }
}
