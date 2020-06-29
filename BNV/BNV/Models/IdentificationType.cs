using System;
using Newtonsoft.Json;
using Syncfusion.XForms.MaskedEdit;

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

        [JsonProperty("maskExpression")]
        public string MaskExpression { get; set; }
        
        [JsonProperty("order")]
        public long Order { get; set; }

        public string MaskWatermark { get; internal set; }

        public MaskType MaskType { get; internal set; }
    }
}
