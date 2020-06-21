using Newtonsoft.Json;

namespace BNV.Models
{
    public class Gender
    {
        [JsonProperty("codIdGenero")]
        public long CodIdGenero { get; set; }

        [JsonProperty("desIdGenero")]
        public string DesIdGenero { get; set; }
    }
}
