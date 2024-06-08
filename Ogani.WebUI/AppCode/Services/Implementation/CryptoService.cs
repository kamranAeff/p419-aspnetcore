using Microsoft.Extensions.Options;
using Ogani.WebUI.Models.Configurations;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Ogani.WebUI.AppCode.Services.Implementation
{
    public class CryptoService : ICryptoService
    {
        private readonly CryptoServiceConfiguration options;

        public CryptoService(IOptions<CryptoServiceConfiguration> options)
        {
            this.options = options.Value;
        }

        public string Encrypt(string value, bool appliedUrlEncode = false)
        {
            using (var symProvider = new TripleDESCryptoServiceProvider())
            using (var md5 = MD5.Create())
            {
                byte[] valueBuffer = Encoding.UTF8.GetBytes(value);

                byte[] keyBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"{options.Key}202$"));
                byte[] iVBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"202$_#$@{options.Key}"));

                var transformer = symProvider.CreateEncryptor(keyBuffer, iVBuffer);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                {
                    cs.Write(valueBuffer, 0, valueBuffer.Length);
                    cs.FlushFinalBlock();

                    ms.Position = 0;
                    ms.Seek(0, SeekOrigin.Begin);

                    byte[] bytes = new byte[ms.Length];

                    ms.Read(bytes, 0, bytes.Length);

                    string chiperText = Convert.ToBase64String(bytes);


                    if (appliedUrlEncode)
                    {
                        chiperText = HttpUtility.UrlEncode(chiperText);
                    }

                    return chiperText;
                }
            }
        }

        public string Decrypt(string chiperText)
        {
            using (var symProvider = new TripleDESCryptoServiceProvider())
            using (var md5 = MD5.Create())
            {
                byte[] valueBuffer = Convert.FromBase64String(chiperText);

                byte[] keyBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"{options.Key}202$"));
                byte[] iVBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"202$_#$@{options.Key}"));

                var transformer = symProvider.CreateDecryptor(keyBuffer, iVBuffer);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                {
                    cs.Write(valueBuffer, 0, valueBuffer.Length);
                    cs.FlushFinalBlock();

                    ms.Position = 0;
                    ms.Seek(0, SeekOrigin.Begin);

                    byte[] bytes = new byte[ms.Length];

                    ms.Read(bytes, 0, bytes.Length);

                    string pureText = Encoding.UTF8.GetString(bytes);

                    return pureText;
                }
            }
        }
    }
}
