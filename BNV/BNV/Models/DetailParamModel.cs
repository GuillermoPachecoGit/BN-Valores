using Refit;

namespace BNV.Models
{
    public class DetailParamModel
    {
        [AliasAs("time")]
        public int Time { get; set; }
    }
}
