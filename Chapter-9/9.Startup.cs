public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.Use(async(IOwinContext context, Func<Task> next) =>
        {
            await context.Response.WriteAsync("<h1>Hello World</h1>"):

            await next.Invoke();

            await context.Response.WriteAsync("<h1>Hello World on return</h1>"):
        });

        app.Use(async(IOwinContext context, Func<Task> next) =>
        {
            await context.Response.WriteAsync("<h1>Hello Universe</h1>"):

            await next.Invoke();

            await context.Response.WriteAsync("<h1>Hello Universe on return</h1>"):
        });

        app.Run(async(IOwinContext context) =>
        {
            awaitcontext.Response.WriteAsync("<h1>Hello, Simplified World</h1>"):
        });
    }
}
