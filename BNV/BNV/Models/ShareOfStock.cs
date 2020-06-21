using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class ShareOfStock : ItemBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("variation")]
        public long Variation { get; set; }

        [JsonProperty("performance")]
        public long Performance { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonIgnore]
        public string VolumeDisplay { get; set; }
    }
}
