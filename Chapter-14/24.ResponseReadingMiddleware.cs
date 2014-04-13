using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
            IOwinContext context = new OwinContext(env);

            // Switch the response body Stream to a memory Stream
            var originalStream = context.Response.Body;
            var responseBuffer = new MemoryStream();
            context.Response.Body = responseBuffer;

            await this.next(env);

            // Seek to the beginning and read the Stream
            responseBuffer.Seek(0, SeekOrigin.Begin);
            string responseBody = await this.ReadAllAsync(responseBuffer);
            Console.WriteLine(responseBody);

            // Seek to the beginning again and copy the contents into the original stream
            responseBuffer.Seek(0, SeekOrigin.Begin);
            await responseBuffer.CopyToAsync(originalStream);
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