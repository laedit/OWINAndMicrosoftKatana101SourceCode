using System.Net.Http;
using System.Web.Http;

namespace NancyAndWebApi
{
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse<string>("Hello Employee");
        }
    }
}