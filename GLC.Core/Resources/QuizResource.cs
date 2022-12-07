using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class QuizResource
    {

        public Guid QuizId { get; set; }
        public string Type { get; set; }
        public int Mark { get; set; }
        public int Level { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }

        public Guid? StudentId { get; set; }
        public StudentResource Student { get; set; }

        public Guid? SubjectID { get; set; }
        public SubjectResource? Subject { get; set; }
        public ICollection<StudentQuizQuestionBankResource>? QuizeQuestions = new Collection<StudentQuizQuestionBankResource>();
    }
}
