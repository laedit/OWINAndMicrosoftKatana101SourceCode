using System.Net.Http;
using System.Web.Http;

namespace MyWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse<Employee>(new Employee()
            {
                Id = id,
                FirstName = "Johnny",
                LastName = "Law"
            });
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}