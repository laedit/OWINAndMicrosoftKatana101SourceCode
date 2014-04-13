using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/planets", helloApp =>
            {
                helloApp.Use(async (IOwinContext context, Func<Task> next) =>
                {
                    await context.Response.WriteAsync("<h1>Hello Mercury</h1>");

                    await next.Invoke();

                    await context.Response.WriteAsync("<h1>Hello Mercury on return</h1>");
                });

                helloApp.Run(async (IOwinContext context) =>
                {
                    await context.Response.WriteAsync("<h1>Hello Neptune</h1>");
                });
            });

            app.Run(async(IOwinContext context) =>
            {
                await context.Response.WriteAsync("<h1>Hello Universe</h1>");
            });
        }
    }
}