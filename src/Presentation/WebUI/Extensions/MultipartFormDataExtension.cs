using System.IO;
using System.Net.Http.Headers;

namespace WebUI.Extensions
{
    public static partial class Extension
    {
        static public MultipartFormDataContent AddString(this MultipartFormDataContent content, string value, string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(value))
                content.Add(new StringContent(value), fieldName);
            return content;
        }

        static public MultipartFormDataContent AddInt(this MultipartFormDataContent content, int? value, string fieldName)
        {
            if (value.HasValue)
                content.Add(new StringContent($"{value.Value}"), fieldName);
            return content;
        }

        static public MultipartFormDataContent AddByte(this MultipartFormDataContent content, byte? value, string fieldName)
        {
            if (value.HasValue)
                content.Add(new StringContent($"{value.Value}"), fieldName);
            return content;
        }

        static public MultipartFormDataContent AddDecimal(this MultipartFormDataContent content, decimal? value, string fieldName)
        {
            if (value.HasValue)
                content.Add(new StringContent($"{value.Value}"), fieldName);
            return content;
        }

        static public MultipartFormDataContent AddBoolean(this MultipartFormDataContent content, bool? value, string fieldName)
        {
            if (value.HasValue && value.Value)
                content.Add(new StringContent("true"), fieldName);
            return content;
        }

        static public MultipartFormDataContent AddFile(this MultipartFormDataContent content, IFormFile value, string fieldName)
        {
            using (var ms = new MemoryStream())
            {
                value.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var fileContent = new ByteArrayContent(ms.ToArray());
                if (!string.IsNullOrEmpty(value.ContentType))
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(value.ContentType);

                content.Add(fileContent, fieldName, value.FileName);
            }

            return content;
        }
        static public MultipartFormDataContent AddFileAsBase64(this MultipartFormDataContent content, IFormFile value, string fieldName)
        {
            using (var ms = new MemoryStream())
            {
                value.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] fileBytes = ms.ToArray();

                string base64String = Convert.ToBase64String(fileBytes);

                content.AddString($"data:{value.ContentType};base64,{base64String}", fieldName);
            }

            return content;
        }
    }
}