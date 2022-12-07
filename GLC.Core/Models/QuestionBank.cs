using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GLC.Cores.Models
{
    public class QuestionBank
    {
        [Key]
        public Guid QuestionId { get; set; }
        [Required]
        public string Title { get; set; }
        public int QuestionMark { get; set; }
        public int Level { get; set; }
     

        [MaxLength(50)]
        public string CorrectAnswer { get; set; }
        public ICollection<StudentQuizeQuestionBank> QuizeQuestion { get; set; } = new Collection<StudentQuizeQuestionBank>();
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new Collection<QuestionAnswer>();
        public ICollection<QuestionCategory> QuestionCategory { get; set; } = new Collection<QuestionCategory>();

    }
}
