public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.Run((IOwinContext context) =>
        {
            return context.Response.WriteAsync("<h1>Hello World</h1>");
        });

        app.Run((IOwinContext context) =>
        {
            return context.Response.WriteAsync("<h1>Hello, Simplified World</h1>");
        });
    }
}


