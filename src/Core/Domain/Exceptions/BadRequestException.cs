namespace Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; private set; }

        public BadRequestException(string message, Dictionary<string, IEnumerable<string>> errors)
            : base(message) => this.Errors = errors;
    }
}
