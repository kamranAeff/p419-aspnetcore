namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base(Localization.Resources.Messages.Message.UnauthorizedMessage)
        {
        }
    }
}
