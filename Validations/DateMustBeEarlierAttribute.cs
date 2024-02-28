using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MetransHomework2.Validations
{
    /// <summary>
    /// Adds custom validation attribute that verifies that DateTime is earlier than other DateTime property.
    /// </summary>
    public class DateMustBeEarlierAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        /// <summary>
        /// Required constructor.
        /// </summary>
        /// <param name="comparisonProperty">Property to compare DateTime object with</param>
        public DateMustBeEarlierAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        /// <summary>
        /// Method doing the validation
        /// </summary>
        /// <param name="value">Value to be compared with comparison property</param>
        /// <param name="validationContext">Context of validation</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Exception is being thrown in case the property for comparison spec</exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty) ?? throw new ArgumentException("Property with this name not found");

            object? comparison_object = comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (value == null ||  value is not DateTime || comparison_object == null || comparison_object is not DateTime)
                throw new ArgumentException("This validation attribute can be used only for DateTime properties.");

            var currentValue = (DateTime)value;            

            var comparisonValue = (DateTime)comparison_object;

            if (currentValue < comparisonValue)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage ?? $"Date must be earlier than {_comparisonProperty}");
        }
    }
}
