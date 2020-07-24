using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class ShareOfStock : ItemBase
    {
        [JsonIgnore]
        public string Sender { get; set; }
    }
}
