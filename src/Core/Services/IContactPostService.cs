namespace Services
{
    public interface IContactPostService
    {
        Task<string> Add(string fullName, string email, string message);
    }
}
