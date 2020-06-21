using System;
using Refit;

namespace BNV.Models
{
    public class ItemsParamModel
    {
        [AliasAs("currency")]
        public long? Currency { get; set; }

        [AliasAs("sector")]
        public long? Sector { get; set; }
    }
}
