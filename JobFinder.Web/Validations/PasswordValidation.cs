using JobFinder.Web.ChainOfResponsability;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            // Build the validation chain
            var lengthValidator = new LengthValidationHandler(8);
            var uppercaseValidator = new UppercaseValidationHandler();
            var numberValidator = new NumberValidationHandler();
            var specialCharValidator = new SpecialCharacterValidationHandler();

            lengthValidator.SetNextValidator(uppercaseValidator);
            uppercaseValidator.SetNextValidator(numberValidator);
            numberValidator.SetNextValidator(specialCharValidator);

            // Validate the password
            if (!lengthValidator.Validate(password, out string errorMessage))
            {
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
