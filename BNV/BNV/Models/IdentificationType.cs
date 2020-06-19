using System;
namespace BNV.Models
{
    public class IdentificationType
    {
        public int  CodType {get ; set; }

        public string Description { get; set; }

        public string Mask { get; set; }

        public int Order { get; set; }

        public string RegEx { get; set; }
        public string MaskWatermark { get; internal set; }
    }
}
