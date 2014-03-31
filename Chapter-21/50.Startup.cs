using Owin;

namespace HelloIoC
{
    public class Startup
    {
        private readonly IClock clock = null;
        
        public Startup(IClock clock)
        {
            this.clock = clock;
        }
        
        public void Configuration(IAppBuilder app)
        {
            var middleware = new MiddlewareWithDependency(this.clock);
            app.Use(middleware);
        }
    }
}
