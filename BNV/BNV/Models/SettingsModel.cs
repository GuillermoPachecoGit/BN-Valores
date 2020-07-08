using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class SettingsModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("exchangeRateNotify")]
        public long ExchangeRateNotify { get; set; }

        [JsonProperty("bonosNotify")]
        public long BonosNotify { get; set; }

        [JsonProperty("accionesNotify")]
        public long AccionesNotify { get; set; }

        [JsonProperty("homeScreen")]
        public long HomeScreen { get; set; }

        [JsonProperty("currency")]
        public long Currency { get; set; }

        [JsonProperty("sector")]
        public long Sector { get; set; }
    }
}
