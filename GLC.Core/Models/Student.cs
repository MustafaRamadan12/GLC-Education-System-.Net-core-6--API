using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; } = Guid.NewGuid();
        [MaxLength(50)]

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        public string? barcode { get; set; }
        public byte[]? Image { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        public string? ConfirmPassword { get; set; }
        [MaxLength(55)]
        public string? Address { get; set; }
        public string? ParentEmail { get; set; }
        public int Age { get; set; }
        public int Level { get; set; }
        [MaxLength(11)]
        public string? Phone { get; set; }
        public string Gender { get; set; }
        [ForeignKey("Group")]
        public Guid? GroupID { get; set; }
        public Group? Group { get; set; }
        public ICollection<StudentQuizeQuestionBank> Quizes { get; set; } = new Collection<StudentQuizeQuestionBank>();
        public ICollection<ChatingDetails> ChatingDetails { get; set; } = new Collection<ChatingDetails>();
        public DateTime? AssignDate { get; set; }

        //public Student()
        //{
        //    StudentId = Guid.NewGuid();
        //}
    }
}
