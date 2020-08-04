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
        public string Price { get; set; }

        [JsonProperty("variation")]
        public string Variation { get; set; }

        [JsonProperty("performance")]
        public string Performance { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonIgnore]
        public string VolumeDisplay { get; set; }

        [JsonIgnore]
        public string VariationDisplay { get; set; }

        [JsonIgnore]
        public string PriceDisplay { get; set; }

        [JsonIgnore]
        public string Title { get; set; }

        [JsonIgnore]
        public string DateDisplay { get; internal set; }
    }
}
