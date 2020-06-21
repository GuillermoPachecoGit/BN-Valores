using Refit;

namespace BNV.Models
{
    public class LoginParam
    {
        [AliasAs("tipid")]
        public long TipId { get; set; }

        [AliasAs("username")] // TODO USERNAME???? y string??
        public string Id { get; set; }

        [AliasAs("password")] // todo deberia ser un string
        public string Password { get; set; }

        [AliasAs("grant_type")]
        public string Grand_type => "password";
    }
}
