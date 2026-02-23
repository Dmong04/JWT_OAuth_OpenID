using APPLICATION.services;
using APPLICATION.Use_cases.Login_cases;
using DOMAIN.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Handlers
{
    public class LoginHandler(IUserRepository userRepository, AuthService auth,
        PasswordService passwordService)
    {
        public async Task<ClaimsPrincipal?> Login(LoginRequest request)
        {
            var user = await userRepository.GetUserByUsername(request.Username);
            var verify_password = passwordService.VerifyHashedPassword(user, user.Password, request.Password);
            if (verify_password == PasswordVerificationResult.Failed) return null;
            var principal = AuthService.ClaimsPrincipal(user);
            return principal;
        }
    }
}
