using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class Quiz
    {
        [Key]
        public Guid QuizId { get; set; }
        public string Type { get; set; }
        public int Mark { get; set; }
        public int Level { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }
       
        [ForeignKey("Subject")]
        public Guid? SubjectID { get; set; }
        public  Subject? Subject { get; set; }
        public ICollection<StudentQuizeQuestionBank> QuizeQuestions = new Collection<StudentQuizeQuestionBank>();

    }
}
