using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace trade.application.auth
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //COMENT
            context.Validated();
            //await Task.Run(() => { });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("session_id", Guid.NewGuid().ToString()));
            //identity.AddClaim(new Claim("username", context.UserName));

            //context.Validated(identity);
        }
        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary) { context.AdditionalResponseParameters.Add(property.Key, property.Value); }
            foreach (var item in context.Identity.Claims) { context.AdditionalResponseParameters.Add(item.Type, item.Value); }
            return base.TokenEndpointResponse(context);
        }
    }
}