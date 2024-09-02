namespace Domain.Exceptions
{
    public class UnverifiedPhoneException : Exception
    {
        public UnverifiedPhoneException() 
            : base("Your phone appears to be unverified")
        {
        }
    }
}
