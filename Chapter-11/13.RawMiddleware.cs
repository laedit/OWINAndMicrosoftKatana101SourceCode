using System;
using System.Collection.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyAnotherHost
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    
    public class RawMiddleware
    {
        private readonly AppFunc next;

        public RawMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
             byte[] bytes = Encoding.UTF8.GetBytes("<h1>Hello Raw</h1>");

             var response = (Stream)env["owin.ResponseBody"];
             await response.WriteAsync(bytes, 0, bytes.Length);
             await this.next(env);
        }
    }
}

