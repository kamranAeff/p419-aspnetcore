using System.Text.RegularExpressions;

namespace Application.Extensions
{
    public static partial class Extension
    {
        static public string ToSlug(this string context)
        {
            if (string.IsNullOrWhiteSpace(context))
                return null;

            var replaceSet = new Dictionary<string, string>() {
                {"Ü|ü", "u"},
                {"İ|I|ı", "i"},
                {"Ş|ş", "s"},
                {"Ö|ö", "o"},
                {"Ç|ç", "c"},
                {"Ğ|ğ", "g"},
                {"Ə|ə", "e"},
                {"#", "sharp"},
                {@"(\?|/|\|\.|'|`|%|\*|!|@|\+)+", ""},
                {@"\&+", "and"},
                {@"[^a-z0-9]+", "-"},
                };

            return replaceSet.Aggregate(context, (i, m) => Regex.Replace(i, m.Key, m.Value, RegexOptions.IgnoreCase))
                .ToLower();
        }

        static public string ClearHtml(this string value, int len = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                goto l1;

            //<[^>]*>

            value = Regex.Replace(value, @"<[^>]*>", "");

            if (len > 20 && value.Length > len)
                value = $"{value.Substring(0, len)}...";


            l1:
            return value;
        }
    }
}
