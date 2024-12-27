using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SignalRHub : Hub
{
    private static Dictionary<string, string> OnlineUsers = new Dictionary<string, string>();

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (!OnlineUsers.ContainsKey(userId))
        {
            OnlineUsers[userId] = Context.ConnectionId;
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        OnlineUsers.Remove(userId ?? "");

        await base.OnDisconnectedAsync(exception);
    }

    public Dictionary<string, string> GetOnlineUsers()
    {
        return OnlineUsers;
    }

    public async Task CallUser(string callerId, string receiverId, string offer)
    {
        if (OnlineUsers.ContainsKey(receiverId))
        {
            await Clients.Client(OnlineUsers[receiverId]).SendAsync("ReceiveOffer", callerId, offer);
        }
    }

    public async Task SendAnswer(string callerId, string answer)
    {
        if (OnlineUsers.ContainsKey(callerId))
        {
            await Clients.Client(OnlineUsers[callerId]).SendAsync("ReceiveAnswer", answer);
        }
    }

    public async Task SendIceCandidate(string callerId, string candidate)
    {
        if (OnlineUsers.ContainsKey(callerId))
        {
            await Clients.Client(OnlineUsers[callerId]).SendAsync("ReceiveCandidate", candidate);
        }
    }

    public async Task HangUp(string userId)
    {
        if (OnlineUsers.ContainsKey(userId))
        {
            await Clients.Client(OnlineUsers[userId]).SendAsync("CallEnded");
        }
    }
}
