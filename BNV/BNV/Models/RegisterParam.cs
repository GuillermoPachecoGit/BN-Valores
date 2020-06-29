using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class RegisterParam
    {
        [JsonProperty("tipid")]
        public long Tipid { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("country")]
        public long Country { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }
    }
}
