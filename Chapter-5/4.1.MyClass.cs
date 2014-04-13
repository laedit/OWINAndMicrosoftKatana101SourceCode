using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace HelloWorld
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    
    public class MyClass
    {
        public void Run(IAppBuilder app)
        {
            var middleware = new Func<AppFunc, AppFunc>(Middleware);
            app.Use(middleware);
        }

        // Called during pipeline building
        public AppFunc Middleware(AppFunc nextMiddleware)
        {
            AppFunc appFunc = (IDictionary<string, object> env) =>
            {
                // Called per-request.
                // env is the environment dictionary.
                byte[] bytes = Encoding.UTF8.GetBytes("<h1>Hello World</h1>");

                var headers = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];
                headers["Content-Length"] = new [] { bytes.Length.ToString() };
                headers["Content-Type"] = new [] { "text/html" };
                
                var response = (Stream)env["owin.ResponseBody"];
                response.WriteAsync(bytes, 0, bytes.Length);
                return nextMiddleware(env);
            };

            return appFunc;
        }
    }
}

