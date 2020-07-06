using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JwtTest2.Models
{
    public class UserAuth
    {
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
