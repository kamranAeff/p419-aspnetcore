using System.Diagnostics;

namespace WebIntroEmpty2.AppCode.LoggingConcept
{
    public class EmailLogger : IMyLogger
    {
        public EmailLogger()
        {
            this.InstanceId = Guid.NewGuid();
        }
        public Guid InstanceId { get; }
        public void WriteLog(string message)
        {
            Debug.WriteLine($"{typeof(EmailLogger).Name}: {message}");
        }
    }
}
