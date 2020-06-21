﻿using System;
using Newtonsoft.Json;

namespace BNV.Models
{
    public class IdentificationType
    {
        [JsonProperty("codIdType")]
        public long CodIdType { get; set; }

        [JsonProperty("desIdType")]
        public string Description { get; set; }

        [JsonProperty("mask")]
        public string Mask { get; set; }

        [JsonProperty("regExpression")]
        public string RegExpression { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        public string MaskWatermark { get; internal set; }
    }
}
