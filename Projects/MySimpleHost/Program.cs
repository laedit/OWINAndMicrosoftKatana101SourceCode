﻿using System;
using Microsoft.Owin.Hosting;
using HelloWorld;

namespace MySimpleHost
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
}