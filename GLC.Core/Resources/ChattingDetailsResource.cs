namespace GLC.Core.Resources
{
  public class ChattingDetailsResource
  {

    public Guid? StId { get; set; }
    public StudentResource? Student { get; set; }

    public Guid? GroupChatId { get; set; }
    public GroupChatResource? GroupChat { get; set; }

    public Guid? GroupId { get; set; }
    public GroupResource? group { get; set; }

    public Guid? TeacherId { get; set; }
    public TeacherResource? Teacher { get; set; }
    public string Message { get; set; }
    public bool? IsSenderStudent { get; set; }
    public DateTime DateTime { get; set; }
  }
}
