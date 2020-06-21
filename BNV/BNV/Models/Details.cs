using System.Collections.Generic;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class Details
    {
        [JsonProperty("tradedVolumeAverage")]
        public string TradedVolumeAverage { get; set; }

        [JsonProperty("tradedVolumeMin")]
        public string TradedVolumeMin { get; set; }

        [JsonProperty("tradedVolumeMax")]
        public string TradedVolumeMax { get; set; }

        [JsonProperty("valueAverage")]
        public double ValueAverage { get; set; }

        [JsonProperty("valueMax")]
        public long ValueMax { get; set; }

        [JsonProperty("valueMin")]
        public long ValueMin { get; set; }

        [JsonProperty("data")]
        public List<ItemData> Data { get; set; }
    }
}
