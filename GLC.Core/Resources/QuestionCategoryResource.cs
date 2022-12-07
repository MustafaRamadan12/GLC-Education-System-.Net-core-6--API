namespace GLC.Core.Resources
{
    public class QuestionCategoryResource
    {

        public Guid QuestionCategoryId { get; set; }

        public string Category { get; set; }

        public Guid? QuestionBankId { get; set; }
        public QuestionBankResource? QuestionBank { get; set; }
    }
}
