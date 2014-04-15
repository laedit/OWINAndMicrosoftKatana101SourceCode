using Microsoft.Owin.Hosting;
using StructureMap;
using System;
using System.Reflection;

namespace HelloIoC
{
    public class Program
    {
        public static void Main()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();
                    scan.Assembly(Assembly.GetExecutingAssembly());
                });
            });

            var startup = ObjectFactory.GetInstance<Startup>();

            using (WebApp.Start("http://localhost:5000", startup.Configuration))
            {
                Console.WriteLine("Server ready... Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}