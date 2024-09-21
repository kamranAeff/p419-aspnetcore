using AutoMapper;

namespace WebApi.MapperConfiguration.Converters
{
    public class FilePathConverter : IValueConverter<string?, string?>
    {
        public string? Convert(string? sourceMember, ResolutionContext context)
        {
            if (sourceMember is null)
                return null;

            string host = Environment.GetEnvironmentVariable("SOURCE_URL");

            if (!string.IsNullOrWhiteSpace(host))
                return $"{host}/files/{sourceMember}";

            if (context.TryGetItems(out Dictionary<string, object> items) && items.TryGetValue("host", out object _host))
                host = _host?.ToString();
            else
                host = string.Empty;

            return $"{host}/files/{sourceMember}";
        }
    }
}
