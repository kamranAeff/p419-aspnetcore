using System.Diagnostics;

namespace WebIntroEmpty2.AppCode.LoggingConcept
{
    public class TextLogger : IMyLogger
    {
        public TextLogger()
        {
            this.InstanceId = Guid.NewGuid();
        }
        public Guid InstanceId { get; }

        public void WriteLog(string message)
        {
            Debug.WriteLine($"{typeof(TextLogger).Name}: {message}");
        }
    }
}
