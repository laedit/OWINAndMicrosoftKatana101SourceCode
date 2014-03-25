using Nancy;

public class HomeModule : NancyModule
{
    pulbic HomeModule()
    {
        Get["/"] = x =>
        {
            return "<h1>Hello Home</h1>";
        };
    }
}
