using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class TeacherResource
    {

        public Guid TeacherId { get; set; }

        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }

        //public string? ConfirmPassword { get; set; }

        //public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? School { get; set; }
        public byte[]? PhotoPath { get; set; }
        public ICollection<ChattingDetailsResource>? ChatingDetails { get; set; } = new Collection<ChattingDetailsResource>();
        public ICollection<SubjectResource>? subjects { get; set; } = new Collection<SubjectResource>();
        public ICollection<VideoResource>? videos { get; set; } = new Collection<VideoResource>();
    }
}
