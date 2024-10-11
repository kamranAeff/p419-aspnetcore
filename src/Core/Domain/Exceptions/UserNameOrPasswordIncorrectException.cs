using Localization.Resources.Messages;

namespace Domain.Exceptions
{
    public class UserNameOrPasswordIncorrectException : Exception
    {
        public UserNameOrPasswordIncorrectException() 
            : base(Localization.Resources.Messages.Message.UserNameOrPasswordIncorrectMessage)
        {
        }
    }
}
