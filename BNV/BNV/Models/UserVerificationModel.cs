using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class UserVerificationModel
    {
        [JsonProperty("EsCliOAutor")]
        public long EsCliOAutor { get; set; }

        [JsonProperty("TieneCorreo")]
        public long TieneCorreo { get; set; }
    }
}
