using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Country
    {
        [Key]
        public int Country_id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<User>? Users { get; set; }
    }
}
