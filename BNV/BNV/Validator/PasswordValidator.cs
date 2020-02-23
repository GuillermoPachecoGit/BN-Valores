using System;
using BNV.Interfaces;

namespace BNV.Validator
{
    public class PasswordValidator : IValidationRule<string>
    {
        const int minLength = 6;
        public string Description =>
        $"Password should be at least {minLength } characters long.";

        public bool Validate(string value)
        {
            if (value == string.Empty) return true;
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
        }

}
}
