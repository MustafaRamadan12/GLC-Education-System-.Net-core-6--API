using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GLC.Cores.Models
{
  public class GroupChat
  {
    [Key]
    public Guid GroupChatId { get; set; }
    public string Message { get; set; }
    public int level { get; set; }
    public ICollection<ChatingDetails> Chats { get; set; } = new Collection<ChatingDetails>();

  }
}
