namespace Domain.Exceptions
{
    public class UnverifiedEmailException : Exception
    {
        public UnverifiedEmailException() 
            : base(Localization.Resources.Messages.Message.UnverifiedEmailMessage)
        {
        }
    }
}
