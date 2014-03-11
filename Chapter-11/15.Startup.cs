public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.Use<ImprovedMiddleware>(new GreetingOptions()
        {
            Message = "Hello from ImprovedMiddleware",
            IsHtml = true
        }); // Register our middlware.

         // Rest of the middleware registrations go here.
    }
}

