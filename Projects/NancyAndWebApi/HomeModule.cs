using Nancy;

namespace NancyAndWebApi
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = x =>
            {
                return "<h1>Hello Home</h1>";
            };
        }
    }
}