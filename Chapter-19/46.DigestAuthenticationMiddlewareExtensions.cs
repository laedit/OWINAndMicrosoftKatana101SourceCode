using Microsoft.Owin.Extensions;

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