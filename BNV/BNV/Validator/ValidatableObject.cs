using System;
using System.Collections.Generic;
using System.Linq;
using BNV.Interfaces;
using PropertyChanged;

namespace BNV.Validator
{
    public class ValidatableObject<T> : BaseViewModel
    {
        // Collection of validation rules to apply
        public List<IValidationRule<T>> Validations { get; }
            = new List<IValidationRule<T>>();


        // The value itself
        public T Value { get; set; }
        // PropertyChanged.Fody will call this method on Value change
        void OnValueChanged() => propertyChangedCallback?.Invoke();

        readonly Action propertyChangedCallback;

        public ValidatableObject(
            Action propertyChangedCallback = null,
            params IValidationRule<T>[] validations)
        {
            this.propertyChangedCallback = propertyChangedCallback;
            foreach (var val in validations)
                Validations.Add(val);
        }

        // PropertyChanged.Fody attribute, on Value change IsValid will change as well
        [DependsOn(nameof(Value))]
        public bool IsValid => Validations.TrueForAll(v => v.Validate(Value));
        // Validation descriptions aggregator
        public string ValidationDescriptions =>
            string.Join(Environment.NewLine, Validations.Select(v => v.Description));
    }
}
