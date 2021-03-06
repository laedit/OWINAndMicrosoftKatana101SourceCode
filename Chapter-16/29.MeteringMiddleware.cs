using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NancyAndWebApi
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class MeteringMiddleware
    {
        private readonly AppFunc next;

        public MeteringMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            IOwinContext context = new OwinContext(env);

            var originalStream = context.Response.Body;
            var responseBuffer = new MemoryStream();
            context.Response.Body = responseBuffer;

            await this.next(env);

            context.Response.ContentLength = responseBuffer.Length;

            Console.WriteLine("Response body to {0} {1} is {2} bytes",
                                   context.Request.Method,
                                   context.Request.Uri,
                                   responseBuffer.Length);

            responseBuffer.Seek(0, SeekOrigin.Begin);

            await responseBuffer.CopyToAsync(originalStream);
        }
    }
}