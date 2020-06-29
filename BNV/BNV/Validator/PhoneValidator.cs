using System;
using System.Collections.Generic;
using System.Linq;
using BNV.Interfaces;
using BNV.Settings;

namespace BNV.Validator
{
    public class PhoneValidator : IValidationRule<string>
    {
        private List<char> _validCharacters = new List<char> { '-', '(', ')'};
        
        public string Description { get; set; }

        public bool Validate(string value)
        {
            if (value == string.Empty) return true;
            if (value.ToCharArray().Any(x => !_validCharacters.Contains(x) && !char.IsDigit(x) && !char.IsWhiteSpace(x)))
            {
                Description = MessagesAlert.ErrorPhoneInvalid;
                return true;
            }
            if (string.IsNullOrWhiteSpace(value)) {
                Description = MessagesAlert.ErrorPhoneInvalid;
                return false;
            }

            if (value.Length <= 20)
            {
                Description = MessagesAlert.ErrorPhoneLenght;
                return true;
            }

            return  false;
        }
    }
}
