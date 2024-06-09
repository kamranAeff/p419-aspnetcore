namespace Services
{
    public interface IContactPostService
    {
        string Add(string fullName, string email, string message);
    }
}
