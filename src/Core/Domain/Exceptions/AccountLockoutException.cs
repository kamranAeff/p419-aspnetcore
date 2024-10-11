namespace Domain.Exceptions
{
    public class AccountLockoutException : Exception
    {
        public AccountLockoutException() 
            : base(Localization.Resources.Messages.Message.AccountLockoutMessage)
        {
        }
    }
}
