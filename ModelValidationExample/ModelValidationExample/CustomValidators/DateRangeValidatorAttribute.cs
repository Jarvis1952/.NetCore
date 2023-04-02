using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ModelValidation.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string? OtherPrpoertyName { get; set; }
        public DateRangeValidatorAttribute(string otherPropertyName)
        {
            OtherPrpoertyName = otherPropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime to_date = Convert.ToDateTime(value);
                // Gets the Value of FromDate  
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPrpoertyName ?? string.Empty);
                if (otherProperty != null)
                {
                    DateTime from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

                    if (from_date > to_date)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                return null;
            }
            else
                return null;
        }
    }
}
