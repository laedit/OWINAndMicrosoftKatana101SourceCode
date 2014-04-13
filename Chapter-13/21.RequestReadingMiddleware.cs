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

            // Buffer the request body
            var requestBuffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBuffer);
            requestBuffer.Seek(0, SeekOrigin.Begin);

            context.Request.Body = requestBuffer;

            // Read the body
            var reader = new StreamReader(context.Request.Body);
            string content = await reader.ReadToEndAsync();

            // Seek to the beginning of the stream for the
            // other components to correctly read the request body.
            ((MemoryStream)context.Request.Body).Seek(0, SeekOrigin.Begin);

            Console.WriteLine(content);

            await this.next(env);
        }
    }
}