using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Login_cases
{
    public class LoginRequest
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
