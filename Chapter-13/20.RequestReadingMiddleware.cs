using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyAnotherHost
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    
    public class RequestReadingMiddleware
    {
        private readonly AppFunc next;

        public RequestReadingMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            IOwinContext context = new OwinContext(env);

            var reader = new StreamReader(context.Request.Body);
            string content = await reader.ReadToEndAsync();
            
            Console.WriteLine(content);
            
            await this.next(env);
        }
    }
}