using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.User_cases.Find
{
    public class UserResponse
    {
        public int? Id { get; set; }

        public string? Username { get; set; }

        public string? Role { get; set; }

        public Country? Country { get; set; }
    }
}
