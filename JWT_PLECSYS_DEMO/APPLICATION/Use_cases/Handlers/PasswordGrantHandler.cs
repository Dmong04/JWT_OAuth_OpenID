using APPLICATION.Use_cases.Login_cases;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Handlers
{
    public class PasswordGrantHandler(LoginHandler handler) : IOpenIddictServerHandler<OpenIddictServerEvents.HandleTokenRequestContext>
    {
        public async ValueTask HandleAsync(OpenIddictServerEvents.HandleTokenRequestContext context)
        {
            if (!context.Request.IsPasswordGrantType()) return;
            var username = context.Request.Username;
            var password = context.Request.Password;

            var principal = await handler.Login(new LoginRequest 
            {
                Username = username!,
                Password = password!
            });
            if (principal is null)
            {
                context.Reject(
                    error: OpenIddictConstants.Errors.InvalidGrant,
                    description: "Usuario y/o contraseña inválidos");
                return;
            }
            context.SignIn(principal);
        }
    }
}
