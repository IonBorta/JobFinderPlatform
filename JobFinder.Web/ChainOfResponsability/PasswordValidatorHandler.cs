namespace JobFinder.Web.ChainOfResponsability
{
    public abstract class PasswordValidatorHandler
    {
        protected PasswordValidatorHandler _nextPasswordValidatorHandler;
        public void SetNextValidator(PasswordValidatorHandler passwordValidatorHandler)
        {
            _nextPasswordValidatorHandler = passwordValidatorHandler;
        }
        public virtual bool Validate(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (_nextPasswordValidatorHandler != null)
            {
                return _nextPasswordValidatorHandler.Validate(password, out errorMessage);
            }

            return true;
        }
    }
}
