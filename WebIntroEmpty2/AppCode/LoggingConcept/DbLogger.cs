using System.Diagnostics;

namespace WebIntroEmpty2.AppCode.LoggingConcept
{
    public class DbLogger : IMyLogger
    {
        public DbLogger()
        {
            this.InstanceId = Guid.NewGuid();
        }
        public Guid InstanceId { get; }

        public void WriteLog(string message)
        {
            Debug.WriteLine($"{typeof(DbLogger).Name}: {message}");
        }
    }
}
