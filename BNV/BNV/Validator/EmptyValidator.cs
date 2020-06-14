using System;
using BNV.Interfaces;

namespace BNV.Validator
{
    public class EmptyValidator : IValidationRule<string>
    {
        public string Description => "Debe completar todos los campos";

        public bool Validate(string value)
        {
            if (value == string.Empty) return true;
            if (string.IsNullOrWhiteSpace(value)) return false;
            return true;
        }
    }
}
