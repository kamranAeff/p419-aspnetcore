using Microsoft.AspNetCore.SignalR;

namespace SignalRIntro.Hubs
{
    public interface IGameHubRule
    {
        Task ReceiveAsync(string msg);
        Task ReceivePositionAsync(string json);
    }
    public class GameHub : Hub<IGameHubRule>
    {
        public override async Task OnConnectedAsync()
        {
            /*
             await Clients.Caller.SendAsync("receive", $"Salamlar, {Context.ConnectionId}");
            await Clients.Others.SendAsync("receive", $"Teze adam qosuldu, {Context.ConnectionId}");
             */

            await Clients.Caller.ReceiveAsync($"Salamlar, {Context.ConnectionId}");
            await Clients.Others.ReceiveAsync($"Teze adam qosuldu, {Context.ConnectionId}");

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string msg)
        {
            await Clients.Others.ReceiveAsync(msg);
        }

        public async Task SendToMessage(string msg, string userId)
        {
            await Clients.Client(userId).ReceiveAsync(msg);
        }

        public async Task SendPosition(string json)
        {
            await Clients.Others.ReceivePositionAsync(json);
        }
    }
}
