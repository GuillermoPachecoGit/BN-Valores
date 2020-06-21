using Refit;

namespace BNV.Models
{
    public class UserVerifyParam
    {
        [AliasAs("tipId")]
        public long IdentificationType { get; set; }

        [AliasAs("Id")]
        public string Identification { get; set; }
    }
}
