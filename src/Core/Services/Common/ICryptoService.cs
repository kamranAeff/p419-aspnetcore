namespace Services.Common
{
    public interface ICryptoService
    {
        string Encrypt(string value, bool appliedUrlEncode = false);
        string Decrypt(string cipherText);
    }
}
