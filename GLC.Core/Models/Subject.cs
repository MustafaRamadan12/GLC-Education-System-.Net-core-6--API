using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class Subject
    {
        [Key]
        public Guid SubjectId { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public int Level { get; set; }
        [ForeignKey("Teacher")]
        public Guid? TeacherId { get; set; }
        public  Teacher? Teacher { get; set; }
        public ICollection<Video> videos { get; set; } = new Collection<Video>();
        public ICollection<Group> Groups { get; set; } = new Collection<Group>();
        public ICollection<Quiz> Quizes { get; set; } = new Collection<Quiz>();

    }
}
