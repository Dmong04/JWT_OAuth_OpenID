using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces
{
    public interface ITokenRepository
    {
        Task<Token> GetTokenByUserId(int user_id);

        Task<Token> CreateToken(Token token);
    }
}
