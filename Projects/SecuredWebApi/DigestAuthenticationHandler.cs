﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecuredWebApi
{
    public class DigestAuthenticationHandler : AuthenticationHandler<DigestAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var ticket = new AuthenticationTicket(null, (AuthenticationProperties)null);

            string authHeader = Request.Headers.Get("Authorization");

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Digest", StringComparison.OrdinalIgnoreCase))
            {
                string parameter = authHeader.Substring("Digest ".Length).Trim();
                string userName = null;
                if (DigestAuthenticator.TryAuthenticate(parameter, Request.Method, out userName))
                {
                    var identity = new ClaimsIdentity(new[]
                                       {
                                           new Claim(ClaimTypes.Name, userName),
                                           new Claim(ClaimTypes.NameIdentifier, userName)
                                       },
                                       authenticationType: "Digest");

                    ticket = new AuthenticationTicket(identity as ClaimsIdentity, (AuthenticationProperties)null);
                }
            }

            return Task.FromResult<AuthenticationTicket>(ticket);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                Response.Headers.AppendValues(
                                 "WWW-Authenticate",
                                 String.Format("Digest realm = {0}, nonce={1}", Options.Realm, Options.GenerateNonceBytes().ToMD5Hash()));
            }

            return Task.FromResult<object>(null);
        }
    }
}