using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Owin

namespace MyAnotherHost
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    
    public class ResponseReadingMiddleware
    {
        private readonly AppFunc next;

        public ResponseReadingMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            await this.next(env);

            IOwinContext context = new IOwinContext(env);
            string responseBody = await this.ReadAllAsync(context.Response.Body);
            
            Console.WriteLine(responseBody);
        }
        
        private async Task<string> ReadAllAsync(Stream stream)
        {
            string content = null;
            
            try
            {
                var reader = new StreamReader(stream);
                content = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            } // Add a breakpoint here.
            
            return content;
        }
    }
}
