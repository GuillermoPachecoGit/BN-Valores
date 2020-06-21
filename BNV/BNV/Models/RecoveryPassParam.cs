using Refit;

namespace BNV.Models
{
    public class RecoveryPassParam
    {
        [AliasAs("tipid")]
        public string TipId { get; set; }

        [AliasAs("id")]
        public string Id { get; set; }
    }
}
