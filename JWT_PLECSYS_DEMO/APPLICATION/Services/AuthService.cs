using DOMAIN.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace APPLICATION.services
{
    public class AuthService(IOpenIddictScopeManager scopeManager)
    {
        private readonly IOpenIddictScopeManager _scopeManager = scopeManager;

        public static ClaimsPrincipal ClaimsPrincipal(User user)
        {
            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            identity.SetClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString(), Destinations.AccessToken);
            identity.SetClaim(OpenIddictConstants.Claims.Name, user.Username, Destinations.AccessToken);
            identity.SetClaim(OpenIddictConstants.Claims.Role, user.Role, Destinations.AccessToken);
            if (user.Country != null)
            {
                identity.SetClaim("country", user.Country.Name, Destinations.AccessToken);
            }

            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(new[]
            {
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.OfflineAccess
            });

            principal.SetResources("PLECSYS_API_RESOURCE");
            return principal;
        }
    }
}
