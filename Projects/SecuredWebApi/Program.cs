using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.Jwt;

namespace SecuredWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("Server ready... Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // New code added - BEGIN
            var jwtOptions = new JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { "http://localhost:5000/api" },
                IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(
                    issuer: "http://authzserver.demo",
                    base64Key: "tTW8HB0ebW1qpCmRUEOknEIxaTQ0BFCYrdjOdOI4rfM=")
            }
            };
            app.UseJwtBearerAuthentication(jwtOptions);
            // New code added - END

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            app.UseWebApi(config);
        }
    }

    public class EmployeesController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse<string>("Hello Employee");
        }
    }
}