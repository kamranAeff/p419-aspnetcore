namespace Domain.Exceptions
{
    public class AccountLockoutException : Exception
    {
        public AccountLockoutException() 
            : base("Your account is temporarily locked due to multiple failed logins")
        {
        }
    }
}
