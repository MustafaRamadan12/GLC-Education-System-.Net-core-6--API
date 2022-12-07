using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class GroupChatResource
    {

        public Guid GroupChatId { get; set; }
        public string? Message { get; set; }
        public int level { get; set; }
        public ICollection<ChattingDetailsResource>? Chats { get; set; } = new Collection<ChattingDetailsResource>();
    }
}
