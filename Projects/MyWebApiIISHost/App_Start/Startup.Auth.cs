using Owin;
using SecuredWebApi;
using System.Security.Cryptography;

namespace MyWebApiIISHost
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            var digestOptions = new DigestAuthenticationOptions()
            {
                Realm = "magical",
                GenerateNonceBytes = () =>
                {
                    byte[] bytes = new byte[16];
                    using (var provider = new RNGCryptoServiceProvider())
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