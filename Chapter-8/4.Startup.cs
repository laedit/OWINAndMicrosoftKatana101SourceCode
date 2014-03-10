using System;
using System.Threading.Tasks;
using System.Text;
using Owin;
using Microsoft.Owin;

// Good practice to specify the startup explicitly
// even if the class and methods are named
// according to the convention.
[assembly: OwinStartup(typeof(HelloWorldV2.Startup))]
namespace HelloWorldV2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run((IOwinContext context) =>
            {
                byte[] bytes = Encoding.UTF8.GetBytes("<h1>Hello World</h1>");

                var response = context.Response;
                response.ContentType = "text/html";
                response.ContentLength = bytes.length;

                return response.WriteAsync(bytes);
            });
        }
    }
}

