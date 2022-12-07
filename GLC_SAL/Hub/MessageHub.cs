using GLC.Core.Resources;
using GLC_SAL.IHub;
using Microsoft.AspNetCore.SignalR;
namespace GLC_SAL.Hub
{
  public class MessageHub : Hub<IMessageHub>
  {
    public async Task SendMessagesToUser(IEnumerable<MessageResource> gc)
    {

      await Clients.All.SendMessagesToUser(gc);
    }
    public async Task ReceiveMessageFromUser(string message)
    {
      await Clients.All.ReceiveMessageFromUser(message);
    }
  }
}
