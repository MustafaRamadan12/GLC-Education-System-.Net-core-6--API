using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class Video
    {
        [Key]
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string MainSection { get; set; }
        public string MainSubject { get; set; }
        public string Link { get; set; }
        [ForeignKey("Teacher")]
        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        [ForeignKey("Subject")]
        public Guid? SubjectId { get; set; }
        public  Subject? Subject { get; set; }
    }
}
