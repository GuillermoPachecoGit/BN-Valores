using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class Sector
    {
        [JsonProperty("codIdSector")]
        public long CodIdSector { get; set; }

        [JsonProperty("desIdSector")]
        public string DesIdSector { get; set; }
    }
}
