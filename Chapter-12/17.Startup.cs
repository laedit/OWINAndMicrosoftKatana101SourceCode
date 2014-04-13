using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          // Configured to run first
          app.Use<MachineNamingMiddleware>();
          
          app.Use(async (IOwinContext context, Func<Task> next) =>
          {
              // Simulate File Not Found
              context.Response.StatusCode = 404;
              await next.Invoke();
          });
          
          app.Run(async (IOwinContext context) =>
          {
              await context.Response.WriteAsync("<h1>Hello Universe</h1>");
          });
        }
    }
}