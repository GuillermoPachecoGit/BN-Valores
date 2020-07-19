using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class ItemBase
    {
        [JsonIgnore]
        public string ColorStatus { get; set; }

        [JsonIgnore]
        public string Arrow { get; set; }

        [JsonIgnore]
        public string Triangle { get; set; }

        [JsonIgnore]
        public bool IsBlue { get; set; }

        [JsonIgnore]
        public bool IsRed { get;  set; }

        [JsonIgnore]
        public bool IsGreen { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("variation")]
        public double Variation { get; set; }

        [JsonProperty("performance")]
        public double Performance { get; set; }

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

        [JsonIgnore]
        public string VariationDisplay { get; set; }

        [JsonIgnore]
        public string PriceDisplay { get; set; }
    }
}
