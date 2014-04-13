using Microsoft.Owin.Hosting;
using System;

namespace MyAnotherHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // This line loads MyWebApi assembly
            var controllerType = typeof(MyWebApi.Controllers.EmployeesController);

            using (WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("Server ready... Press Enter to quit.");

                Console.ReadLine();
            }
        }
    }
}