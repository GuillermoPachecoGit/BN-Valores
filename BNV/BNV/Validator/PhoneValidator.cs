using System;
using BNV.Interfaces;

namespace BNV.Validator
{
    public class PhoneValidator : IValidationRule<string>
    {
        public string Description => "Teléfono debe contener 13 dígitos";

        public bool Validate(string value)
        {
            if (value == string.Empty) return true;
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.Length == 13 ;
        }
    }
}
