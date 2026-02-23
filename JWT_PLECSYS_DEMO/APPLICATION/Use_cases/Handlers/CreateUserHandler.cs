using APPLICATION.services;
using APPLICATION.Use_cases.User_cases.Create;
using APPLICATION.Use_cases.User_cases.Find;
using DOMAIN.Entities;
using DOMAIN.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Handlers
{
    public class CreateUserHandler(IUserRepository userRepository, 
        ICountryRepository countryRepository, PasswordService passwordService)
    {
        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await userRepository.GetAllUsers();
            if (users is null) return null;
            var response = users.Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role,
                Country = u.Country
            });
            return response;
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var user = await userRepository.GetUserById(id);
            if (user is null) return null;
            var response = new UserResponse
            {
                Id = id,
                Username = user.Username,
                Role = user.Role,
                Country = user.Country
            };
            return response;
        }

        public async Task<UserResponse> CreateUser(NewUserRequest request)
        {
            var found = await userRepository.GetUserByUsername(request.Username);
            if (found is not null) return null;
            var created = new User
            {
                Username = request.Username,
                Role = request.Role,
                Country_id = request.Country_id
            };
            created.Password = passwordService.HashPassword(created, request.Password);
            await userRepository.CreateUser(created);
            var country = await countryRepository.GetCountryById(created.Country_id);
            var success = new UserResponse
            {
                Id = created.Id,
                Username = created.Username,
                Role = created.Role,
                Country = new Country
                {
                    Country_id = country.Country_id,
                    Name = country.Name
                }
            };
            return success;
        }
    }
}
