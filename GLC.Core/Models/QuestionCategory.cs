using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLC.Cores.Models
{
    public class QuestionCategory
    {
        [Key]
        public Guid QuestionCategoryId { get; set; }
        [MaxLength(10)]
        public string Category { get; set; }
        [ForeignKey("QuestionBank")]
        public Guid?QuestionBankId { get; set; }
        public  QuestionBank? QuestionBank { get; set; }

    }
}
