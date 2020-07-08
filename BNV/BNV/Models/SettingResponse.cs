using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class SettingResponse
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("exchangeRateNotify")]
        public double ExchangeRateNotify { get; set; }

        [JsonProperty("bonosNotify")]
        public double BonosNotify { get; set; }

        [JsonProperty("accionesNotify")]
        public double AccionesNotify { get; set; }

        [JsonProperty("homeScreen")]
        public long HomeScreen { get; set; }

        [JsonProperty("currency")]
        public long Currency { get; set; }

        [JsonProperty("sector")]
        public long Sector { get; set; }
    }
}
