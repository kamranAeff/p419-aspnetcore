using Application.Services;
using Domain.Configurations;
using Microsoft.Extensions.Options;
using Services.Registration;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Services
{
    [SingletonLifeTime]
    class CryptoService : ICryptoService, IDisposable
    {
        private readonly CryptoServiceConfiguration options;
        private readonly HashAlgorithm ha;
        private readonly HashAlgorithm haSha1;
        private readonly SymmetricAlgorithm csp;
        private bool disposed = false;

        public CryptoService(IOptions<CryptoServiceConfiguration> options)
        {
            this.options = options.Value;
            ha = MD5.Create();
            haSha1 = SHA1.Create();
            csp = TripleDES.Create();

            var keyBytes = ha.ComputeHash(Encoding.UTF8.GetBytes($"{this.options.Key}202$"));
            var newKeyBytes = new byte[24]; // TripleDES: 192 bit = 24 byte
            Array.Copy(keyBytes, newKeyBytes, Math.Min(keyBytes.Length, newKeyBytes.Length));
            csp.Key = newKeyBytes;

            csp.Mode = CipherMode.ECB;
            csp.Padding = PaddingMode.PKCS7;
        }

        public string Encrypt(string value, bool applyUrlEncode = false)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            var cipherBytes = csp.CreateEncryptor()
                                 .TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            var cipherText = Convert.ToBase64String(cipherBytes);

            if (applyUrlEncode)
                cipherText = HttpUtility.UrlEncode(cipherText);

            return cipherText;
        }

        public string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var decryptedBytes = csp.CreateDecryptor()
                                    .TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public string Sha1Hash(string value) => string.Join(string.Empty, haSha1.ComputeHash(Encoding.UTF8.GetBytes(value)).Select(b => b.ToString("x2")));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                ha.Dispose();
                csp.Dispose();
            }

            disposed = true;
        }

    }
}
