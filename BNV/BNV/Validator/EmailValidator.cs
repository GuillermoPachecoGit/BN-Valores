using System;
using System.Text.RegularExpressions;
using BNV.Interfaces;

namespace BNV
{
    public class EmailValidator : IValidationRule<string>
    {
        const string pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";

        public string Description => "Please enter a valid email.";

        public bool Validate(string value)
        {
            if (value == string.Empty) return true;
            if (string.IsNullOrWhiteSpace(value)) return false;
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(value);
        }
    }
}
