using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class StudentResource
    {
        public Guid StudentId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Email { get; set; }
        public string? barcode { get; set; }
        public byte[]? Image { get; set; }
        public string? Password { get; set; }


        public string? ConfirmPassword { get; set; }

        public string? Address { get; set; }
        public string? ParentEmail { get; set; }
        public int Age { get; set; }
        public int Level { get; set; }

        public string? Phone { get; set; }
        public string Gender { get; set; }
        public Guid? GroupID { get; set; }
        public GroupResource? Group { get; set; }
        public ICollection<StudentQuizQuestionBankResource>? Quizes { get; set; } = new Collection<StudentQuizQuestionBankResource>();
        public ICollection<ChattingDetailsResource>? ChatingDetails { get; set; } = new Collection<ChattingDetailsResource>();
        public DateTime? AssignDate { get; set; }
    }
}
