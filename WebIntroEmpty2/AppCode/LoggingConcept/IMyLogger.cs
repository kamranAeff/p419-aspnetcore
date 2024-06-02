namespace WebIntroEmpty2.AppCode.LoggingConcept
{
    public interface IMyLogger
    {
        Guid InstanceId { get; }
        void WriteLog(string message);
    }
}
