namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName)
            : base(string.Format(Localization.Resources.Messages.Message.NotFoundMessage, entityName))
        {

        }
    }
}
