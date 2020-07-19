using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class LoginTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("isTempPass")]
        public long IsTemp { get; set; }

    }
}
