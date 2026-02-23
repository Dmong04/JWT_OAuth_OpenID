using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.User_cases.Create
{
    public class NewUserRequest
    {
        public required string Username { get; set; }

        public required string Password { get; set; }

        public required string Role { get; set; }

        public required int Country_id { get; set; }
    }
}
