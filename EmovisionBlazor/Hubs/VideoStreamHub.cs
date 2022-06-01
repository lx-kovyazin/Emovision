using Microsoft.AspNetCore.SignalR;

namespace EmovisionBlazor.Hubs;

public class VideoStreamHub : Hub
{
    public const string Uri = "/VideoStreamHub";

    public async Task Broadcast(string user, string message)
    {
        Console.WriteLine($"Broadcast\n{{\n\t user:{user}\n\t message:{message}\n}}");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}
