using Owin;
using SecuredWebApi;
using Syste:Security.Cryptography;

[assembly: OwinStartup(typeof(MyWebApiIISHost.Startup))]
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder)
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
        }
    }
}
