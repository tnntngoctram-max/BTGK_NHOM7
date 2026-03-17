using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTGK_NHOM7.ToeicExams
{
    public class Answer : FullAuditedEntity<int>
    {
        public int QuestionId { get; set; }
        public string Label { get; set; } // A, B, C hoặc D
        public string Content { get; set; }
        public bool IsCorrect { get; set; } // True nếu là đáp án đúng

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}