using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Use_cases.Token_cases.Find
{
    internal class TokenResponse
    {
        public string? Access_token { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Expires_in { get; set; }

        public string? Refresh_token { get; set; }

        public string? Token_id { get; set; }
    }
}
