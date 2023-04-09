using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidation.CustomValidators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} can not be Empty")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} Should be between {2} to {1} characters")]
        //[RegularExpression("^[A-Zaz]$", ErrorMessage = "{0} Only Contains alphabets, dots and space ")]
        public string? PersonName { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid {0}")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Enter Valid {0}")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} Can not be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Re-Enter Password")]
        [Compare("Password", ErrorMessage = "{0} and {1} does not match, Re-enter Password ")]
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double? Price { get; set; }

        [MinimumYearValidator(2000, ErrorMessage = "Date of Birth is not in valid format")]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be greater than or equal to 'To Date'")]
        //[BindNever] 
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                yield return new ValidationResult("You must enter either DateOfBirth or Age", new[] { nameof(Age) });
            }
        }

        public override string ToString()
        {
            return $"PersonName:{PersonName}, Email:{Email}, Phone:{Phone}, Password:{Password}, Price:{Price}";
        }
    }
}
