namespace JobFinder.Web.ChainOfResponsability
{
    public class UppercaseValidationHandler : PasswordValidatorHandler
    {
        public override bool Validate(string password, out string errorMessage)
        {
            if (!password.Any(char.IsUpper))
            {
                errorMessage = "Password must contain at least one uppercase letter.";
                return false;
            }

            return base.Validate(password, out errorMessage);
        }
    }
}
