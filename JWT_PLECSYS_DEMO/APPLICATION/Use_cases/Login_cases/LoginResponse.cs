using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Login_cases
{
    public class LoginResponse
    {
        public User? User { get; set; }

        public Token? Token { get; set; }
    }
}
