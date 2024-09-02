namespace Application.Services
{
    public interface ICryptoService
    {
        string Encrypt(string value, bool appliedUrlEncode = false);
        string Decrypt(string cipherText);
        string Sha1Hash(string value);
    }
}
