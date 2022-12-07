namespace GLC.Core.Resources
{
    public class QuestionAnswerResource
    {

        public Guid QuestionAnswerId { get; set; }
        public string QuestionAnsWer { get; set; }

        public Guid? QuestionId { get; set; }
        public QuestionBankResource? QuestionBank { get; set; }
    }
}
