using Microsoft.Owin;
using Owin;
using System;
using System.IO;
using System.Text;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<RequestReadingMiddleware>();

            app.Run(async (IOwinContext context) =>
            {
                string body = String.Empty;

                using (var reader = new StreamReader(context.Request.Body))
                {
                    body = await reader.ReadToEndAsync();
                }

                context.Response.ContentLength = Encoding.UTF8.GetByteCount(body);

                await context.Response.WriteAsync(body);
            });
        }
    }
}