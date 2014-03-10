public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.Use((IOwinContext context, Func<Task> next) =>
        {
            return context.Response.WriteAsync("<h1>Hello World</h1>"):

            // Call next int the pipeline
            return next.Invoke();
        });

        app.Run((IOwinContext context) =>
        {
            return context.Response.WriteAsync("<h1>Hello, Simplified World</h1>"):
        });
    }
}
