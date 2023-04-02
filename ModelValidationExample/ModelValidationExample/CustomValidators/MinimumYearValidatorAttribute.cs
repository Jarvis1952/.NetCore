using System.ComponentModel.DataAnnotations;

namespace ModelValidation.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public int MinimumYear { get; set; }
        public string DefaultErrorMessage { get; set; } = "Year Should not be less than {0}";
        public MinimumYearValidatorAttribute()
        {

        }
        public MinimumYearValidatorAttribute(int minimumYear)
        {
            MinimumYear = minimumYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime)value;
                if (dateTime.Year >= MinimumYear)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}
