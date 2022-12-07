using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
  public class ChatingDetails
  {
    [ForeignKey("Student")]
    public Guid? StId { get; set; }
    public Student? Student { get; set; }
    [ForeignKey("GroupChat")]
    public Guid? GroupChatId { get; set; }
    public GroupChat GroupChat { get; set; }
    [ForeignKey("group")]
    public Guid? GroupId { get; set; }
    public Group? group { get; set; }
    [ForeignKey("Teacher")]
    public Guid? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public string? Message { get; set; }
    public bool? IsSenderStudent { get; set; }
    public DateTime? DateTime { get; set; }
  }
}
