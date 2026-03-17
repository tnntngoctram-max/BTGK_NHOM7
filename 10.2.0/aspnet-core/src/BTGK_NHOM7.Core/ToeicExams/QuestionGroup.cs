using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTGK_NHOM7.ToeicExams
{
    public class QuestionGroup : FullAuditedEntity<int>
    {
        public int ExamId { get; set; }
        public int PartNumber { get; set; } // Lưu Part 5, 6 hoặc 7
        public string PassageText { get; set; } // Nội dung đoạn văn (nếu có)

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}