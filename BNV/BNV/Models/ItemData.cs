using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class ItemData
    {
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }
    }
}
