using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using trade;

[assembly: OwinStartup(typeof(Startup))]
namespace trade
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/api/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
            //    Provider = new AuthorizationServerProvider()
            //};
            //app.UseOAuthAuthorizationServer(OAuthServerOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}