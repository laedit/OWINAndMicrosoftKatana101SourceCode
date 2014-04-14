using Microsoft.Owin.Security.Jwt;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        // New code added - BEGIN
        var jwtOptions = new JwtBearerAuthenticationOptions
        {
            AllowedAudiences = new [] {"http://localhost:5000/api" },
            IssuerSecurityTokenProviders = new []
            {
                new SymmetricKeyIssuerSecurityTokenProvider(
                    issuer: "http://authzserver.demo",
                    base64Key: "tTW8HB0ebW1qpCmRUEOknEIxaTQ0BFCYrdjOdOI4rfM=")
            }
        };
        app.UseJwtBearerAuthentication(jwtOptions);
        // New code added - END
        
        var config = new HttpConfiguration();
        config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
        app.UseWebApi(config);
    }
}