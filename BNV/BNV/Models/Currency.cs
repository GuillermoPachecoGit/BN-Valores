using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class Currency
    {
        [JsonProperty("codIdCurrency")]
        public long CodIdCurrency { get; set; }

        [JsonProperty("desIdCurrency")]
        public string DesIdCurrency { get; set; }
    }
}
