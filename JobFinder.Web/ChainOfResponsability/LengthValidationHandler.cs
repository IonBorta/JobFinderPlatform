using Humanizer;

namespace JobFinder.Web.ChainOfResponsability
{
    public class LengthValidationHandler : PasswordValidatorHandler
    {
        private readonly int _minLength;

        public LengthValidationHandler(int minLength)
        {
            _minLength = minLength;
        }

        public override bool Validate(string password, out string errorMessage)
        {
            if (password.Length < _minLength)
            {
                errorMessage = $"Password must be at least {_minLength} characters long.";
                return false;
            }

            return base.Validate(password, out errorMessage);
        }
    }
}
