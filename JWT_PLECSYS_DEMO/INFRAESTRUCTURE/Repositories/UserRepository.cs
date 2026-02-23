using DOMAIN.Entities;
using DOMAIN.Interfaces;
using INFRAESTRUCTURE.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRAESTRUCTURE.Repositories
{
    public class UserRepository(AppDBContext _ctx) : IUserRepository
    {
        public async Task<User> CreateUser(User user)
        {
            var found = await GetUserByUsername(user.Username);
            if (found != null) return null;
            await _ctx.AddAsync<User>(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _ctx.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
