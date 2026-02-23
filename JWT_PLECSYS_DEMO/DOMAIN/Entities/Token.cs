using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Token
    {
        [Key]
        [ForeignKey("user_id")]
        public int User_id { get; set; }

        public string? Access_token { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Expires_in { get; set; }

        public string? Refresh_token { get; set; }

        public string? Token_id { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
