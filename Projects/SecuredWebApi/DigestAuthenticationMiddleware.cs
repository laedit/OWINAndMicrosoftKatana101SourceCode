using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace SecuredWebApi
{
    public class DigestAuthenticationMiddleware : AuthenticationMiddleware<DigestAuthenticationOptions>
    {
        public DigestAuthenticationMiddleware(OwinMiddleware next, DigestAuthenticationOptions options)
            : base(next, options)
        { }

        protected override AuthenticationHandler<DigestAuthenticationOptions> CreateHandler()
        {
            return new DigestAuthenticationHandler();
        }
    }

    public static class DigestAuthenticationMiddlewareExtensions
    {
        public static IAppBuilder UseDigestAuthentication(this IAppBuilder app, DigestAuthenticationOptions options)
        {
            app.Use(typeof(DigestAuthenticationMiddleware), options);

            // Line added
            app.UseStageMarker(PipelineStage.Authenticate);

            return app;
        }
    }
}