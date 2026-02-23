using DOMAIN.Entities;
using DOMAIN.Interfaces;
using INFRAESTRUCTURE.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRAESTRUCTURE.Repositories
{
    public class TokenRepository(AppDBContext _ctx) : ITokenRepository
    {
        public async Task<Token?> CreateToken(Token token)
        {
            var found = GetTokenByUserId(token.User_id);
            if (found != null) return null;
            _ctx.Tokens.Add(token);
            await _ctx.SaveChangesAsync();
            return token;
        }

        public async Task<Token?> GetTokenByUserId(int user_id)
        {
            return await _ctx.Tokens.FindAsync(user_id);
        }
    }
}
