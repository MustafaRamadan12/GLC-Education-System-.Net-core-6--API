using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class Teacher
    {
        [Key]
        public Guid TeacherId { get; set; }
        [MaxLength(50)]

        public string? Name { get; set; }
        public int? Age { get; set; }
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string ?Email { get; set; }
        [NotMapped]
        public string ?ConfirmPassword { get; set; }

        public string ?Password { get; set; }
        public string? Phone { get; set; } 
        public string? School { get; set; }
        public byte[]? PhotoPath { get; set; }
        public ICollection<Group> Groups { get; set; } = new Collection<Group>();
        public ICollection<ChatingDetails>ChatingDetails { get; set; }=new Collection<ChatingDetails>();
        public ICollection<Subject> subjects { get; set; } = new Collection<Subject>();
        public ICollection<Video> videos { get; set; } = new Collection<Video>();  
    }
}
