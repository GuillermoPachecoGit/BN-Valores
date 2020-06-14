using System;
using BNV.Interfaces;

namespace BNV.Validator
{
    public class PasswordValidator : IValidationRule<string>
    {
        const int minLength = 8;
        public string Description =>
        $"Contraseñas deben contener al menos {minLength} caracteres";

        public bool Validate(string value)
        {
            if (value == string.Empty) return true;
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
        }

}
}
