using AutoMapper;

namespace WebApi.MapperConfiguration.Converters
{
    public class DateTimeValueConverter : IValueConverter<DateTime?, string?>
    {
        public string? Convert(DateTime? sourceMember, ResolutionContext context)
        {
            string format;

            if (context.TryGetItems(out Dictionary<string, object> items) && items.TryGetValue("dateFormat", out object _format))
                format = _format?.ToString();
            else
                format = "dd.MM.yyyy";

            return sourceMember?.ToString(format);
        }
    }
}