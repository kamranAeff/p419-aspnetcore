namespace Domain.Configurations
{
    public class EmailConfiguration
    {
        public required string DisplayName { get; set; }
        public required string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
