using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Net.Http;
using System.Web.Http;

namespace SecuredWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            using(WebApp.Start<Startup>("http://localhost:5000"))
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