using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{ 
    public class QuestionAnswer
    {
        [Key]
        public Guid QuestionAnswerId { get; set; }
        public string QuestionAnsWer { get; set; }
        [ForeignKey("QuestionBank")]
        public Guid? QuestionId { get; set; }
        public  QuestionBank? QuestionBank { get; set; }

    }
}
