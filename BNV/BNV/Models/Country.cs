using Newtonsoft.Json;

namespace BNV.Models
{
    public class Country
    {
        [JsonProperty("codIdPais")]
        public long CodIdPais { get; set; }

        [JsonProperty("desIdPais")]
        public string DesIdPais { get; set; }
    }
}
