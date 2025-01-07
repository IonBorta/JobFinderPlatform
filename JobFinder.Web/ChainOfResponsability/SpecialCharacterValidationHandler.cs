namespace JobFinder.Web.ChainOfResponsability
{
    public class SpecialCharacterValidationHandler : PasswordValidatorHandler
    {
        public override bool Validate(string password, out string errorMessage)
        {
            if (!password.Any(char.IsSymbol) && !password.Any(char.IsPunctuation))
            {
                errorMessage = "Password must contain at least one special character.";
                return false;
            }

            return base.Validate(password, out errorMessage);
        }
    }
}
