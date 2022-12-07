using GLC.Core.Resources;

namespace GLC_SAL.IHub
{
  public interface IMessageHub
  {
    Task SendMessagesToUser(IEnumerable<MessageResource> gc);
    Task ReceiveMessageFromUser(string message);
  }
}
