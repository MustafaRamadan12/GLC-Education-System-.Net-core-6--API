using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class GroupResource
    {

        public Guid GroupId { get; set; }
        public string Day { get; set; }
        public DateTime StartDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Capacity { get; set; }
        public int NumOfStudents { get; set; }

        public Guid? SubjectId { get; set; }
        public SubjectResource? Subject { get; set; }
        public bool Availability { get; set; }
        public ICollection<StudentResource>? Students { get; set; } = new Collection<StudentResource>();
        public ICollection<ChattingDetailsResource>? ChatingDetails { get; set; } = new Collection<ChattingDetailsResource>();

        public Guid? TeacherID { get; set; }
        public TeacherResource? Teacher { get; set; }
    }
}
