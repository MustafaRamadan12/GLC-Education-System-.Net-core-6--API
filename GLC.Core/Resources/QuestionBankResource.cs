using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class QuestionBankResource
    {

        public Guid QuestionId { get; set; }
        public int QuestionMark { get; set; }
        public int Level { get; set; }

        public string CorrectAnswer { get; set; }
        public ICollection<StudentQuizQuestionBankResource>? QuizeQuestion { get; set; } = new Collection<StudentQuizQuestionBankResource>();
        public ICollection<QuestionAnswerResource>? QuestionAnswers { get; set; } = new Collection<QuestionAnswerResource>();
        public ICollection<QuestionCategoryResource>? QuestionCategory { get; set; } = new Collection<QuestionCategoryResource>();
    }
}
