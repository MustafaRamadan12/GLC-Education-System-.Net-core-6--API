using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class Group
    {
        [Key]
        public Guid GroupId{ get; set; }
        public string Day{ get; set; }
        public DateTime? StartDate{ get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Capacity { get; set; }
        public int NumOfStudents { get; set; }
        [ForeignKey("Subject")]
        public Guid? SubjectId { get; set; }
        public  Subject? Subject { get; set; }
        public bool Availability { get; set; }
        public ICollection<Student>? Students { get; set; }=new Collection<Student>();
        public ICollection<ChatingDetails> ChatingDetails { get; set; } = new Collection<ChatingDetails>();
        [ForeignKey("Teacher")]
        public Guid?TeacherID { get; set; }
        public Teacher? Teacher { get; set; }

    }
}
