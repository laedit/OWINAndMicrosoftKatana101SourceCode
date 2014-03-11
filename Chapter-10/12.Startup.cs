using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/planets", helloApp =>
            {
                helloApp.Map("/3", helloEarth =>
                {
                    helloEarth.Run(async(IOwinContext context) =>
                    {
                        await context.Response.WriteAsync("<h1>Hello Earth</h1>");
                    });
                }

                // New code Added - Starts here
                helloApp.MapWhen(context =>
                {
                    if(context.Request.Path.HasValue)
                    {
                        int position;

                        if(Int32.TryParse(context.Request.Path.Value.Trim('/'), out position))
                        {
                            if(position > 8)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                },
                helloPluto =>
                {
                    helloPluto.Run(async(IOwinContext context) =>
                    {
                        await context.Response.WriteAsync("<h1>Oops! We are out of Solar System</h1>");
                    });
                });
                // New code Added - Ends here

                helloApp.Use(async(IOwinContext context, Func<Task> next) =>
                {
                    await context.Response.WriteAsync("<h1>Hello Mercury</h1>");

                    await next.Invoke();

                    await context.Response.WriteAsync("<h1>Hello Mercury on return</h1>");
                });

                helloApp.Run(async(IOwinContext context) =>
                {
                    await context.Response.WriteAsync("<h1>Hello Neptune</h1>");
                });
            }

            app.Run(async(IOwinContext context) =>
            {
                await context.Response.WriteAsync("<h1>Hello Universe</h1>");
            });
        }
    }
}


