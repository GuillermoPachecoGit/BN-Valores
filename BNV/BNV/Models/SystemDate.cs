using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public partial class SystemDate
    {
        [JsonProperty("TipRubro")]
        public string TipRubro { get; set; }

        [JsonProperty("DesRubro")]
        public string DesRubro { get; set; }

        [JsonProperty("FecReferencia")]
        public DateTimeOffset FecReferencia { get; set; }

        [JsonProperty("DiasDif")]
        public long DiasDif { get; set; }
    }
}
