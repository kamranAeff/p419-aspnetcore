using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Extensions
{
    public static partial class Extension
    {
        static public SelectList ToSelectList<T>(this IEnumerable<T>? items, string dataValueField, string dataTextField)
            where T : class
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            return new SelectList(items, dataValueField, dataTextField);
        }
    }
}