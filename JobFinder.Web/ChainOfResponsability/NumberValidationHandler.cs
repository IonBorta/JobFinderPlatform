namespace JobFinder.Web.ChainOfResponsability
{
    public class NumberValidationHandler : PasswordValidatorHandler
    {
        public override bool Validate(string password, out string errorMessage)
        {
            if (!password.Any(char.IsDigit))
            {
                errorMessage = "Password must contain at least one number.";
                return false;
            }

            return base.Validate(password, out errorMessage);
        }
    }
}
