using System;
using Refit;

namespace BNV.Models
{
    public class NewPasswordParam
    {
        [AliasAs("old_password")]
        public string OldPassword { get; set; }

        [AliasAs("password")]
        public string Password { get; set; }

        [AliasAs("pass_confirmation")] 
        public string ConfirmPassword { get; set; }
    }
}
