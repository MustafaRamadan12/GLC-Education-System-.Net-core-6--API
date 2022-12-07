using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class StudentQuizeQuestionBank
    {
        
        [ForeignKey("Quiz")]
        public Guid QuizeId { get; set; }
        public  Quiz Quize { get; set; }
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public  QuestionBank Question { get; set; }
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public Student Student { get;set; }

        public string StudentAnswer { get; set; }
    }
}
