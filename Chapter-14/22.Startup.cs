using System;
using Microsoft.Owin;
using Owin;
using System.Text;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<ResponseReadingMiddleware>();
            
            app.Run(async (IOwinContext context) =>
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes("<h1>Hello World</h1>");
                
                context.Response.ContentLength = bytes.Length;
                
                await context.Response.WriteAsync(bytes);
            });
        }
    }
}
