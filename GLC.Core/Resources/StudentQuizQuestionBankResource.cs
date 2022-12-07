namespace GLC.Core.Resources
{
    public class StudentQuizQuestionBankResource
    {

        public Guid? QuizeId { get; set; }
        public QuizResource Quize { get; set; }

        public Guid? QuestionId { get; set; }
        public QuestionBankResource? Question { get; set; }

        //public Guid? StudentId { get; set; }
        //public StudentResource Student { get; set; }
    }
}
