using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace HelloWorld
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AppFunc middleware = (IDictionary<string, object> env) =>
            {
                var response = (Stream)env["owin.ResponseBody"];
                byte[] bytes = Encoding.UTF8.GetBytes("Hello World");
                return response.WriteAsync(bytes, 0, bytes.Length);
            };
            
            app.Use(middleware);
        }
    }
}
