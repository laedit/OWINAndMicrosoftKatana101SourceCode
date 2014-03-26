public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var digestOptions = new DigestAuthenticationOptions()
        {
            Realm = "magical",
            GenerateNonceBytes = () =>
            {
                byte[] bytes = new byte[16];
                using(var provider = new RNGCryptoServiceProvider())
                {
                    provider.GetBytes(bytes);
                }
                
                return bytes;
            }
        };
        app.UseDigestAuthentication(digestOptions);
        
        var config = new HttpConfiguration();
        config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
        app.UseWebApi(config);
    }
}
