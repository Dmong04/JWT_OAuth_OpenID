using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Token_cases.Create
{
    public class NewTokenRequest
    {
        public required int User_id { get; set; }

        public  required string Access_token { get; set; }

        public required DateTime Created_at { get; set; }

        public required DateTime Expires_in { get; set; }

        public required string Refresh_token { get; set; }

        public required string Token_id { get; set; }
    }
}
