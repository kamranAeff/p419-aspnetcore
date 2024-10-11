namespace Domain.Exceptions
{
    public class UnverifiedPhoneException : Exception
    {
        public UnverifiedPhoneException() 
            : base(Localization.Resources.Messages.Message.UnverifiedPhoneMessage)
        {
        }
    }
}
