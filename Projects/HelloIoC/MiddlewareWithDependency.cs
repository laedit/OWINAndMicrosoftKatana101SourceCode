using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloIoC
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class MiddlewareWithDependency
    {
        private readonly IClock clock = null;
        private AppFunc next;

        public MiddlewareWithDependency(IClock clock)
        {
            this.clock = clock;
        }

        public void Initialize(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            IOwinContext context = new OwinContext(env);

            string message = "Noon";
            if (clock.TimeNow.Hour < 12)
            {
                message = "Ante Meridiem";
            }
            else if (clock.TimeNow.Hour > 12)
            {
                message = "Post Meridiem";
            }

            byte[] bytes = Encoding.UTF8.GetBytes(message);

            await context.Response.WriteAsync(bytes);

            await this.next(env);
        }
    }
}