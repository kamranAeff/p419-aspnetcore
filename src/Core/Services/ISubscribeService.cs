namespace Services
{
    public interface ISubscribeService
    {
        Task<Tuple<bool, string>> Subscribe(string email);
        Task<Tuple<bool, string>> SubscribeApprove(string token);
    }
}
