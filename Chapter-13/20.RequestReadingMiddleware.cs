using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Owin

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
            IOwinContext context = new IOwinContext(env);

            var reader = new StreamReader(context.Request.Body);
            string content = await reader.ReadToEndAsync();
            
            Console.WriteLine(content);
            
            await this.next(env);
        }
    }
}
