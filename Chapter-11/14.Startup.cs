public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.Use<RawMiddleware>(); // Register our middlware.

         // Rest of the middleware registrations go here.
    }
}