using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class Bono : ItemBase
    {
        [JsonIgnore]
        public CoinType Coin { get; set; }

        [JsonIgnore]
        public SectorType Sector { get; set; }

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
    }


    public enum CoinType
    {
        Default=0,
        Dolar=1,
        Colon=2
    }

    public enum SectorType
    {
        All=0,
        Public=1,
        Private=2,
        Mix=3
    }

    public enum MainPageType
    {
        Reportos = 0,
        Bonos = 1,
        TypesExchange = 2,
        Actions = 3
    }
}
