using System;
using Refit;

namespace BNV.Models
{
    public class ItemsParamModel
    {
        [AliasAs("currency")]
        public int Currency { get; set; }

        [AliasAs("sector")]
        public int Sector { get; set; }
    }
}
