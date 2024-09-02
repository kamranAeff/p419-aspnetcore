namespace Domain.Exceptions
{
    public class UnverifiedEmailException : Exception
    {
        public UnverifiedEmailException() 
            : base("Your email appears to be unverified")
        {
        }
    }
}
